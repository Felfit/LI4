using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetChef.Models;

namespace SweetChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        private readonly UtilizadorContext _context;

        // TODO: Corrigir erro
        
        public UtilizadorController(UtilizadorContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Utilizador[] Get()
        {
            return _context.Utilizadores.ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var utilizador = _context.Utilizadores.Find(id);

            if (utilizador == null)
            {
                return NotFound();
            }
            return Ok(utilizador);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Utilizador utilizador)
        {
            _context.Utilizadores.Add(utilizador);
            _context.SaveChanges();
            return new CreatedResult($"/api/utilizador/{utilizador.UtilizadorId}", utilizador);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id)
        {
            var utilizador = _context.Utilizadores.Find(id);

            if (utilizador == null)
            {
                return NotFound();
            }
            _context.Utilizadores.Remove(utilizador);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
