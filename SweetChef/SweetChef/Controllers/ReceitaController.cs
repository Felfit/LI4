using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // GET: api/Utilizador
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
        // Get: api/Utilizador/id
        [HttpGet("{id}", Name = "GetReceita")]
        public ActionResult Get(int id)
        {
            try
            {
                var receita = _context.Receita.Find(id);
                _context.Entry(receita).Collection(b => b.Passo).Load();
                _context.Entry(receita).Collection(b => b.TagReceita).Load();
                _context.Entry(receita).Collection(b => b.ReceitaIngrediente).Load();
                if (receita == null)
                {
                    return NotFound();
                }

                return Ok(receita);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}