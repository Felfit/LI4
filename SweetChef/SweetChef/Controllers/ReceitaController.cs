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
                        Select(ri => new { ri.Ingrediente.Id,ri.Ingrediente.Nome,unidade = ri.Ingrediente.Unidade.Nome}).
                        ToList();
                var utensilios = receita.UtensilioReceita.
                        Select(ur => new { ur.Utensilio.Id, ur.Utensilio.Nome }).
                        ToList();
                List<object> passos = new List<object>();
                foreach(Passo p in receita.Passo){
                    var ings = p.PassoIngrediente.
                            Select(ri => new {ri.Ingrediente.Id,ri.Ingrediente.Nome,unidade = ri.Ingrediente.Unidade.Nome}).
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