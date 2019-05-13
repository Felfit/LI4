using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using SweetChef.ModelsNew;
using Microsoft.EntityFrameworkCore;

namespace SweetChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        //private readonly UtilizadorContext _contexts;

        private readonly SweetContext _context;

        // TODO: Corrigir erro
        
        public UtilizadorController(SweetContext context)
        {
            _context = context;
        }


        [HttpGet("{id}/favoritas")]
        public ActionResult getReceitasFavoritas(int id)
        {
            try
            {
                var utilizador = _context.Utilizador.
                                                Include("Opiniao.Receita").
                                                Where(p => p.Id == id).
                                                FirstOrDefault();
                                                
                if (utilizador == null)
                {
                    return NotFound();
                }

                var receitas = utilizador.Opiniao
                               .Where(o => o.Favorito == true)
                               .Select(o => new { o.Receita })
                               .ToArray();

                return Ok(receitas);
            }
            catch (Exception e)
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
                var user = _context.Utilizador.Find(id);
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
            try
            {
                _context.Utilizador.Add(utilizador);
                _context.SaveChanges();
                return new CreatedResult($"/api/utilizador/{utilizador}", utilizador);
            }  catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // POST: api/Utilizador/autenticar
        [HttpPost]
        [Route("autenticar")]
        public IActionResult Post([FromForm] string email, [FromForm] string password)
        {
            if(email == null || password == null)
            {
                return BadRequest(new
                {
                    emailMissing = email == null,
                    passwordMissing = password == null
                });
            }

            Utilizador utilizador = new Utilizador()
            {
                Email = email,
                Password = password
            };

            try
            {
                utilizador = _context.Utilizador.Where(c => c.Email == utilizador.Email && c.Password == utilizador.Password).Single();
                return Ok(utilizador);
            }
            catch
            {
                return NotFound(email);
            }
        }

        // DELETE: api/Utilizador?5
        [HttpDelete]
        public IActionResult Delete([FromQuery] int codigo)
        {
            try
            {
                var user = _context.Utilizador.Find(codigo);

                if (user == null)
                {
                    return NotFound();
                }
                _context.Utilizador.Remove(user);
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
