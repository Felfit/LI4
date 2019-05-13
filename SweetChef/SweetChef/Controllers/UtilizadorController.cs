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


        //Create Opinion
        [HttpPost("{idUt}/opiniao/{idReceita}")]
        public ActionResult PutOpinion(int idUt, int idReceita, [FromQuery] bool favorito, [FromQuery] short? rating, [FromQuery] bool blacklisted)
        {
            try
            {
                Opiniao o = _context.Opiniao.Find(idUt, idReceita);
                if (o != null)
                {
                    return Created("Object Already Exists",null);
                }
                o = new Opiniao();
                o.Favorito = favorito;
                o.Rating = rating;
                o.Blacklist = blacklisted;
                _context.Opiniao.Add(o);
                return Ok("Succeds");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Get Opinion
        [HttpGet("{idUt}/opiniao/{idReceita}")]
        public ActionResult GetOpinion(int idUt, int idReceita)
        {
            try
            {
                Opiniao o = _context.Opiniao.Find(idUt, idReceita);
                if (o == null)
                {
                    return NotFound();
                }
                return Ok(o);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Update Opinion
        [HttpPut("{idUt}/opiniao/{idReceita}")]
        public ActionResult PutOpinion(int idUt, int idReceita, [FromQuery] bool? favorito, [FromQuery] short? rating, [FromQuery] bool? blacklisted)
        {
            try
            {
                Opiniao o = _context.Opiniao.Find(idUt, idReceita);
                if (o == null)
                {
                    return NotFound();
                }
                if (favorito.HasValue)
                    o.Favorito = (bool)favorito;
                if (rating.HasValue)
                    o.Rating = rating;
                if (blacklisted.HasValue)
                    o.Blacklist = (bool)blacklisted;
                _context.Opiniao.Update(o);
                return Ok();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Get PassoFeedback
        [HttpGet("{idUt}/passoFeedback/{idReceita}/{idPasso}")]
        public ActionResult GetPassoComment(int idUt, int idReceita,int idPasso)
        {
            try
            {
                UtilizadorPasso u = _context.UtilizadorPasso.Find(idUt,idPasso,idReceita);
                if (u == null)
                {
                    return NotFound();
                }
                return Ok(u);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Put PassoFeedback
        [HttpPost("{idUt}/passoFeedback/{idReceita}/{idPasso}")]
        public ActionResult PostPassoComment(int idUt, int idReceita, int idPasso, [FromQuery] int? dificuldade, [FromQuery] string comentario)
        {
            try
            {
                UtilizadorPasso u = _context.UtilizadorPasso.Find(idUt, idPasso, idReceita);
                if (u != null)
                {
                    return Created("Ja foi criado antes usa o pust",null);
                }
                u = new UtilizadorPasso();
                u.Utilizadorid = idUt;
                u.PassoReceitaid = idReceita;
                u.Passoid = idPasso;
                u.Dificuldade = dificuldade;
                u.Comentario = comentario;
                _context.UtilizadorPasso.Add(u);
                return Ok(u);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Post PassoFeedback
        [HttpPut("{idUt}/passoFeedback/{idReceita}/{idPasso}")]
        public ActionResult PutPassoComment(int idUt, int idReceita, int idPasso, [FromQuery] int? dificuldade, [FromQuery] string comentario)
        {
            try
            {
                UtilizadorPasso u = _context.UtilizadorPasso.Find(idUt, idPasso, idReceita);
                if (u == null)
                {
                    return Created("Ja foi criado antes usa o pust", null);
                }
                if(dificuldade.HasValue)
                    u.Dificuldade = dificuldade;
                if(comentario != null)
                    u.Comentario = comentario;
                _context.UtilizadorPasso.Update(u);
                return Ok(u);
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
