using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using SweetChef.ModelsNew;
using Microsoft.EntityFrameworkCore;
using GeoCoordinatePortable;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SweetChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UtilizadorController : ControllerBase
    {
        //private readonly UtilizadorContext _contexts;

        private readonly SweetContext _context;

        // TODO: Corrigir erro
        
        public UtilizadorController(SweetContext context)
        {
            _context = context;
        }

        //Get receitas favoritas
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
                if (receitas.Length == 0)
                    return NotFound();
                else
                    return Ok(receitas);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //Create Execução
        [HttpPost("restricaoAlimentar")]
        public ActionResult PostExecucao([FromForm] int idUt, [FromForm] int idReceita, [FromForm] DateTime data, [FromForm] int duracao)
        {
            try
            {
                Execucao e = _context.Execucao.Find(idUt, idReceita, data);
                if (e != null)
                {
                    return Created("Object Already Exists", null);
                }
                e = new Execucao();
                e.Utilizadorid = idUt;
                e.Receitaid = idReceita;
                e.Data = data;
                e.DuracaoTotal = duracao;
                _context.Execucao.Add(e);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Vai buscar as ementas semanais de uma certa data
        [HttpGet("ementa")]
        public ActionResult GetReceitasEmenta([FromQuery] int idUt, [FromQuery] DateTime data)
        {
            try
            {
                var ementa= _context.EmentaSemanal.
                   Where(em => em.Utilizadorid == idUt && em.Data.Equals(data)).
                   Select(em => em.Receita).
                   ToList();
                return Ok(ementa);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
     
        //Adiciona uma receita à ementa
        [HttpPost("ementa")]
        public ActionResult PostReceitaEmenta([FromForm] int idUt, [FromForm] int idRec, [FromForm] DateTime data)
        {
            try
            {
                EmentaSemanal em = _context.EmentaSemanal.Find(idUt, idRec);
                if(em != null)
                {
                    return Created("Object Already Exists", null);
                }
                em = new EmentaSemanal();
                em.Utilizadorid = idUt;
                em.Receitaid = idRec;
                em.Data = data;
                _context.EmentaSemanal.Add(em);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Remove uma receita da ementa
        [HttpDelete("ementa")]
        public IActionResult Delete([FromForm] int idUt, [FromForm] int idRec, [FromForm] DateTime data)
        {
            try
            {
                //TODO ver se a ordem está correta 
                var ementa = _context.EmentaSemanal.Find(idUt, idRec, data);

                if (ementa == null)
                {
                    return NotFound();
                }
                _context.EmentaSemanal.Remove(ementa);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        //Update à configuração.
        //Remove todas as entradas daquele utilizador nas tabelas respetivas e adiciona as novas.
        //Basicamente se não me passa a lista quer dizer que não quer cenas
        [HttpPut("configuracao")]
        public ActionResult UpdateConfiguracao([FromForm] int idUt, [FromForm] List<int> restricoes, [FromForm] List<int> likes, [FromForm] List<int> dislikes)
        {
            try
            {
                _context.RemoveRange(_context.RestricoesAlimentares.Where(x => x.Utilizadorid == idUt));
                if (restricoes.Count != 0)
                {
                    foreach (int id in restricoes)
                    {
                        RestricoesAlimentares ra = new RestricoesAlimentares();
                        ra.Utilizadorid = idUt;
                        ra.Ingredienteid = id;
                        _context.RestricoesAlimentares.Add(ra);
                       
                    }
                }

                _context.RemoveRange(_context.Likes.Where(x => x.Utilizadorid == idUt));
                if (likes.Count != 0)
                 {
                     foreach (int id in likes)
                     {
                         Likes l = new Likes();
                         l.Utilizadorid = idUt;
                         l.Tagid = id;
                         _context.Likes.Add(l);
                     }
                 }

                _context.RemoveRange(_context.Dislikes.Where(x => x.Utilizadorid == idUt));
                if (dislikes.Count != 0)
                 { 
                     foreach (int id in dislikes)
                     {
                         Dislikes dl = new Dislikes();
                         dl.Utilizadorid = idUt;
                         dl.Tagid = id;
                         _context.Dislikes.Add(dl);
                     }
                 }
                _context.SaveChanges();

                return Ok("Chages where made");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("closestStore/{lat},{lon}")]
        public ActionResult ClosestStore(float lat, float lon) {
            GeoCoordinate usrPos = new GeoCoordinate(latitude : lat,longitude : lon);
            var closest = _context.Lojas.
                OrderBy(p => usrPos.GetDistanceTo(new GeoCoordinate(p.Latitude, p.Longitude))).
                FirstOrDefault();
            if (closest == null)
                return NotFound();
            return Ok(closest);
        }


        
        //Get Opinion
        [HttpGet("opiniao/{idReceita}")]
        public ActionResult GetOpinion(int idReceita)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                var user = _context.Utilizador.Find(idUt);
                bool receitaExists = _context.Receita.Where(r => r.Id == idReceita).Any();
                if (user == null || !receitaExists)
                {
                    return NotFound();
                }
                Opiniao o = _context.Opiniao.Find(idUt, idReceita);
                if (o == null)
                {
                    o = new Opiniao();
                    o.Receitaid = idReceita; o.Utilizadorid = idUt;
                    o.Favorito = false;
                    o.Blacklist = false;
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
        [HttpPut("opiniao/{idReceita}")]
        public ActionResult PutOpinion(int idReceita, [FromForm] bool? favorito, [FromForm] short? rating, [FromForm] bool? blacklisted)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                var user = _context.Utilizador.Find(idUt);
                bool receitaExists = _context.Receita.Where(r => r.Id == idReceita).Any();
                if (user == null || !receitaExists)
                {
                    return NotFound("Receita não existe");
                }
                Opiniao o = _context.Opiniao.Find(idUt, idReceita);
                bool exists = true;
                if (o == null)
                {
                    exists = false;
                    o = new Opiniao();
                    o.Favorito = false;
                    o.Blacklist = false;
                }
                if (favorito.HasValue)
                    o.Favorito = (bool)favorito;
                if (rating.HasValue)
                    o.Rating = rating;
                if (blacklisted.HasValue)
                    o.Blacklist = (bool)blacklisted;
                if (exists)
                    _context.Opiniao.Update(o);
                else
                    _context.Opiniao.Add(o);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Get PassoFeedback
        [HttpGet("passoFeedback/{idReceita}/{idPasso}")]
        public ActionResult GetPassoComment(int idReceita,int idPasso)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                var user = _context.Utilizador.Find(idUt);
                if (user == null)
                {
                    return NotFound();
                }
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
        [HttpPost("passoFeedback/{idReceita}/{idPasso}")]
        public ActionResult PostPassoComment(int idReceita, int idPasso, [FromQuery] string comentario)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                var user = _context.Utilizador.Find(idUt);
                if (user == null)
                {
                    return NotFound();
                }
                UtilizadorPasso u = _context.UtilizadorPasso.Find(idUt, idPasso, idReceita);
                if (u != null)
                {
                    return Created("Ja foi criado antes usa o pust",null);
                }
                u = new UtilizadorPasso();
                u.Utilizadorid = idUt;
                u.PassoReceitaid = idReceita;
                u.Passoid = idPasso;
                u.Comentario = comentario;
                _context.UtilizadorPasso.Add(u);
                _context.SaveChanges();
                return Ok(u);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Post PassoFeedback
        [HttpPut("passoFeedback/{idReceita}/{idPasso}")]
        public ActionResult PutPassoComment(int idReceita, int idPasso, [FromQuery] string comentario)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                var user = _context.Utilizador.Find(idUt);
                if (user == null)
                {
                    return NotFound();
                }
                UtilizadorPasso u = _context.UtilizadorPasso.Find(idUt, idPasso, idReceita);
                if (u == null)
                {
                    return Created("Ja foi criado antes usa o pust", null);
                }
                if(comentario != null)
                    u.Comentario = comentario;
                _context.UtilizadorPasso.Update(u);
                _context.SaveChanges();
                return Ok(u);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("receitasExecutadas")]
        public ActionResult GetExecutados() {
            var sidut = ControllerContext.HttpContext.User.Identity.Name;
            int idUt = Int32.Parse(sidut);
            var user = _context.Utilizador.Find(idUt);
            if (user == null)
            {
                return NotFound();
            }
            var result = _context.Execucao.
                Where(e => e.Utilizadorid == idUt).
                GroupBy(e => e.Receitaid).
                Select(ce => new { numerodevezes = ce.Count(), lastDate = ce.Max(e => e.Data), ce.FirstOrDefault().Receita}).
                ToList();
            return Ok(result);
        }

        [HttpGet("estatisticas/temposMédios")]
        public ActionResult GetTemposTotalMediosExecucaoPorReceita() {
            var sidut = ControllerContext.HttpContext.User.Identity.Name;
            int idUt = Int32.Parse(sidut);
            var user = _context.Utilizador.Find(idUt);
            if (user == null)
            {
                return NotFound();
            }
            var restult = _context.Execucao.
                Where(e => e.Utilizadorid == idUt).
                GroupBy(e => e.Receita.Id).
                Select(lr => new { lr.FirstOrDefault().Receita, tempoMedio = lr.Average(r => r.DuracaoTotal) }).
                ToList();
            return Ok(restult);
        }

        [HttpGet("ingredientesUsados")]
        public ActionResult GetIngredientesUsados() {
            var sidut = ControllerContext.HttpContext.User.Identity.Name;
            int idUt = Int32.Parse(sidut);
            var user = _context.Utilizador.Find(idUt);
            if (user == null)
            {
                return NotFound();
            }
            var result = _context.Execucao.
                Where(e => e.Utilizadorid == idUt).
                SelectMany(e => e.Receita.ReceitaIngrediente).
                GroupBy(ri => ri.Ingredienteid).
                Select(cri => new { quantidade = cri.Sum(s => s.Quantidade) ,
                                    numerodeVezes = cri.Count(),
                                    ingrediente = cri.FirstOrDefault().Ingrediente,
                                    unidade = cri.FirstOrDefault().Ingrediente.Unidade.Nome
                }).
                ToList();
            return Ok(result);
        }

        //Gera lista de compras para os próximos sete dias
        [HttpGet("listaCompras")]
        public ActionResult GetListaDeCompras() {
            var sidut = ControllerContext.HttpContext.User.Identity.Name;
            int idUt = Int32.Parse(sidut);
            var user = _context.Utilizador.Find(idUt);
            if (user == null)
            {
                return NotFound();
            }
            var result = _context.EmentaSemanal.
                Where(es => es.Data >= DateTime.Today && es.Data < DateTime.Today.AddDays(7)).
                SelectMany(es => es.Receita.ReceitaIngrediente).
                GroupBy(ri => ri.Ingredienteid).
                Select(cri => new {
                    quantidade = cri.Sum(s => s.Quantidade),
                    ingrediente = cri.FirstOrDefault().Ingrediente,
                    unidade = cri.FirstOrDefault().Ingrediente.Unidade.Nome
                }).
                ToList();
            return Ok(result);
        }

        
        // GET: api/Utilizador/5
        [HttpGet(Name = "Get")]
        public ActionResult Get()
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                var user = _context.Utilizador.Find(idUt);
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
        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromForm] string email, [FromForm] string password)
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
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, utilizador.Id.ToString()),
                };
                ClaimsIdentity cIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(cIdentity);
                await HttpContext.SignInAsync(principal);
                
                //Request.HttpContext.
                return Redirect("/Home/Cozinhar/");
            }
            catch
            {
                return Redirect("/?email="+email);
            }
        }
        //[HttpPost]
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

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> logoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
        // PUT: api/Utilizador/5
        [HttpPut("{id}")]
        [AllowAnonymous]
        public void Register(int id, [FromBody] string value)
        {
        }
    }
}
