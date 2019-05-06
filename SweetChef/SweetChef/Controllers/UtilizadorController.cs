using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetChef.Models;
using System.Web.Http;

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

        // GET: api/Utilizador
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                Utilizador utilizador = new Utilizador();
                utilizador.Email = "Daniel";
                utilizador.Pass = "Daniel";
                return Ok(utilizador);
                //return Ok(_context.Utilizadores.ToArray());
            } catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/Utilizador/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            try
            {
                var user = _context.Utilizadores.Find(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            } catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Utilizador
        [HttpPost]
        public IActionResult Post([FromForm] Utilizador utilizador)
        {
            return Ok(utilizador);
            /*
            try
            {
                _context.Utilizadores.Add(utilizador);
                _context.SaveChanges();
                return new CreatedResult($"/api/user/{utilizador}", utilizador);
            }  catch(Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            */
        }

        // DELETE: api/Utilizador?5
        [HttpDelete]
        public IActionResult Delete([FromQuery] int codigo)
        {
            try
            {
                var user = _context.Utilizadores.Find(codigo);

                if (user == null)
                {
                    return NotFound();
                }
                _context.Utilizadores.Remove(user);
                _context.SaveChanges();
                return NoContent();
            } catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // PUT: api/Utilizador/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
