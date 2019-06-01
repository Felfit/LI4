﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetChef.ModelsNew;

namespace SweetChef.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReceitaController : Controller
    {
        private readonly SweetContext _context;

        public ReceitaController(SweetContext context)
        {
            _context = context;
        }

        
        [HttpPost("passo/utensilio")]
        public ActionResult addUtensilioPassoReceita([FromForm] UtensilioPasso p)
        {
            try
            {
                ///TODO COISAS
                _context.UtensilioPasso.Add(p);
                if (_context.UtensilioReceita.Where(ur => ur.Utensilioid == p.Utensilioid && ur.Receitaid == p.PassoReceitaid).FirstOrDefault() == null)
                {
                    UtensilioReceita ur = new UtensilioReceita();
                    ur.Receitaid = p.PassoReceitaid;
                    ur.Utensilioid = p.Utensilioid;
                    _context.UtensilioReceita.Add(ur);
                }
                    
                _context.SaveChanges();
                return Redirect("/Home/Editor/" + p.PassoReceitaid + "?passo=" + p.Passoid);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("passo/ingrediente")]
        public ActionResult addIngredientePassoReceita([FromForm] PassoIngrediente p)
        {
            try
            {
                ///TODO COISAS
                _context.PassoIngrediente.Add(p);
                ReceitaIngrediente ri = _context.ReceitaIngrediente.
                    Where(pi => pi.Ingredienteid == p.Ingredienteid && pi.Receitaid == p.PassoReceitaid).
                    FirstOrDefault();
                if (ri == null) {
                    ri = new ReceitaIngrediente();
                    ri.Quantidade = p.Quantidade;
                    ri.Receitaid = p.PassoReceitaid;
                    ri.Ingredienteid = p.Ingredienteid;
                    _context.ReceitaIngrediente.Add(ri);
                }
                else
                {
                    ri.Quantidade += p.Quantidade;
                    _context.ReceitaIngrediente.Update(ri);
                }
                _context.SaveChanges();
                return Redirect("/Home/Editor/" + p.PassoReceitaid + "?passo=" + p.Passoid);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("passo/ingrediente/update")]
        [HttpPut("passo/ingrediente")]
        public ActionResult UpdateIngredientePassoReceita([FromForm] PassoIngrediente p)
        {
            try
            {

                ///FAZER MAIS COISAS
                var old = _context.PassoIngrediente.Find(p.Passoid, p.PassoReceitaid, p.Ingredienteid);
                
                _context.PassoIngrediente.Update(p);
                ReceitaIngrediente ri = _context.ReceitaIngrediente.
                    Where(pi => pi.Ingredienteid == p.Ingredienteid && pi.Receitaid == p.PassoReceitaid).
                    FirstOrDefault();
                ri.Quantidade += p.Quantidade;
                ri.Quantidade -= old.Quantidade;
                _context.ReceitaIngrediente.Update(ri);
                _context.SaveChanges();
                return Redirect("/Home/Editor/" + p.PassoReceitaid + "?passo=" + p.Passoid);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("passo")]
        public ActionResult AddPassoReceita([FromForm] Passo p)
        {
            try
            {
                _context.Passo.Add(p);
                var r = _context.Receita.Find(p.Receitaid);
                r.Tempodepreparacao += p.Duracao;
                _context.Receita.Update(r);
                _context.SaveChanges();
                return Redirect("/Home/Editor/"+p.Receitaid+"?passo="+p.Numero);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("passo/update")]
        [HttpPut("passo")]
        public ActionResult UpdatePassoReceita([FromForm] Passo p)
        {
            try
            {
                _context.Passo.Update(p);
                _context.SaveChanges();
                return Redirect("/Home/Editor/" + p.Receitaid + "?passo=" + p.Numero);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult AddReceita([FromForm] Receita r)
        {
            try
            {
                Console.Beep();
                _context.Receita.Add(r);
                _context.SaveChanges();
                return Redirect("/Home/Editor/" + r.Id);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("update")]
        [HttpPut]
        public ActionResult UpdateReceita([FromForm] Receita r)
        {
            try
            {
                _context.Receita.Update(r);
                _context.SaveChanges();
                return Redirect("/Home/Editor/"+r.Id);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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