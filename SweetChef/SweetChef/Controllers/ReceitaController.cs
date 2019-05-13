using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetChef.ModelsNew;

namespace SweetChef.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : Controller
    {
        private readonly SweetContext _context;

        public ReceitaController(SweetContext context)
        {
            _context = context;
        }

        // GET: api/Receita
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Receita.ToArray());
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //Todas as tags tem de estar presentes na receita
        private bool ContainsTag(ICollection<TagReceita> tagR, List<int> tags)
        {
            if (tagR == null) return false;

            int count = 0;
            foreach (int id in tags)
            {
                foreach (TagReceita t in tagR)
                {
                    if (t.Tag.Id == id)
                    {
                        count++;
                        break;
                    }
                }
                    
            }
            return (count == tags.Count);
        }

        [HttpGet("filtradas")]
        public ActionResult GetReceitasFiltradas([FromQuery] int? dif, [FromQuery]int? dur, [FromQuery] List<int> tags)
        {
            try
            {
                var receitas = _context.Receita.
                                Include("TagReceita.Tag").ToList().
                                Where(x => (!dur.HasValue || x.Tempodepreparacao + x.Tempodeespera <= dur.Value)
                                           && (!dif.HasValue || x.Dificuldade == dif.Value)
                                           && (tags.Count == 0 || ContainsTag(x.TagReceita, tags)));
     
                return Ok(receitas);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("favoritas/{idUtilizador}")]
        public ActionResult GetReceitasFavoritas(int idUtilizador)
        {
            try
            {
                var favoritas = _context.Opiniao.
                    Where(o => o.Utilizadorid == idUtilizador && o.Favorito).
                    Select(o => o.Receita).
                    ToList();
                return Ok(favoritas);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("recomendadas/{idUtilizador}")]
        public ActionResult GetReceitasRecomendadas(int idUtilizador)
        {
            var restricoes = _context.RestricoesAlimentares.
                Where(ra => ra.Utilizadorid == idUtilizador).  //Apenas as restrições do utilizador 
                SelectMany(ra => ra.Ingrediente.ReceitaIngrediente). //Seleciona todas as entradas ReceitaIngrediente 
                Select(ri => ri.Receita);  //Recolhe apenas as receitas.
            var disliked = _context.Dislikes.
                Where(d => d.Utilizadorid == idUtilizador).
                SelectMany(d => d.Tag.TagReceita).
                Select(tr => tr.Receita);
            var blacklisted = _context.Opiniao.
                Where(o => o.Utilizadorid == idUtilizador && o.Blacklist == true).
                Select(o => o.Receita);
            //Seleciona todas as receitas exceto as restritas
            var receitas = _context.Likes.
                Where(l => l.Utilizadorid == idUtilizador).
                SelectMany(l => l.Tag.TagReceita).
                Select(tr => tr.Receita).
                Except(restricoes).
                Except(disliked).
                Except(blacklisted).
                ToList(); 
            return Ok(receitas);
        }

        // Get: api/Receita/id
        [HttpGet("{id}", Name = "GetReceita")]
        public ActionResult GetReceitaEPassos(int id)
        {
            try
            {
                var receita = _context.Receita.
                                                Include("ReceitaIngrediente.Ingrediente.Unidade").
                                                Include("Passo.PassoIngrediente.Ingrediente.Unidade").
                                                Include("UtensilioReceita.Utensilio").
                                                Include("Passo.UtensilioPasso.Utensilio").
                                                Where( p => p.Id == id).
                                                FirstOrDefault();
                if (receita == null)
                {
                    return NotFound();
                }
                var ingredientes = receita.ReceitaIngrediente.
                        Select(ri => new { ri.Ingrediente.Id,ri.Ingrediente.Nome,ri.Quantidade,unidade = ri.Ingrediente.Unidade.Nome}).
                        ToList();
                var utensilios = receita.UtensilioReceita.
                        Select(ur => new { ur.Utensilio.Id, ur.Utensilio.Nome }).
                        ToList();
                List<object> passos = new List<object>();
                foreach(Passo p in receita.Passo){
                    var ings = p.PassoIngrediente.
                            Select(ri => new {ri.Ingrediente.Id,ri.Ingrediente.Nome,ri.Quantidade,unidade = ri.Ingrediente.Unidade.Nome}).
                            ToList();
                    var ut = p.UtensilioPasso.
                            Select(up => new { up.Utensilio.Id, up.Utensilio.Nome }).
                            ToList();
                    passos.Add(new { info = p, ingredientes = ings, utensilios = ut });
                }
                return Ok(new { info = receita,ingredientes,utensilios,passos});
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}