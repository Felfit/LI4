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
        [HttpGet("favoritas")]
        public ActionResult getReceitasFavoritas()
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                var utilizador = _context.Utilizador.
                                                Include("Opiniao.Receita").
                                                Where(p => p.Id == idUt).
                                                FirstOrDefault();
                                                
                if (utilizador == null)
                {
                    return NotFound();
                }

                var receitas = utilizador.Opiniao
                               .Where(o => o.Favorito == true)
                               .Select(o => o.Receita)
                               .ToArray();
                return Ok(receitas);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        //Create Execução
        [HttpPost("execucao")]
        public ActionResult PostExecucao([FromForm] int idReceita, [FromForm] DateTime data, [FromForm] int duracao)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
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
        public ActionResult GetReceitasEmenta([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
        {
           
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                var ementa= _context.EmentaSemanal.
                   Where(em => em.Utilizadorid == idUt && em.Data >= dataInicial && em.Data <= dataFinal).
                   Select(em => new { em.Receita, em.Data}).
                   ToList();
                ementa.Sort((e1, e2) => e1.Data.CompareTo(e2.Data));
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
        public ActionResult PostReceitaEmenta([FromForm] int idRec, [FromForm] DateTime data)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                EmentaSemanal em = _context.EmentaSemanal.Find(data, idRec, idUt);
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
        public IActionResult Delete([FromQuery] int idRec, [FromQuery] DateTime data)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                //TODO ver se a ordem está correta 
                var ementa = _context.EmentaSemanal.Find(data, idRec, idUt);

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


       
        //Basicamente se não me passa a lista quer dizer que não quer cenas
        [HttpPost("configuracao/preferida")]
        public ActionResult AddConfiguracaoPreferida([FromQuery] int preferida)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                Likes l = new Likes();
                l.Utilizadorid = idUt;
                l.Tagid = preferida;
                _context.Likes.Add(l);
                _context.SaveChanges();
                return Ok(l);
             }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("configuracao/preferida")]
        public ActionResult RemoveConfiguracaoPreferida([FromQuery] int preferida)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                Likes l = _context.Likes.Find(idUt, preferida);
                if(l == null)
                {
                    return NotFound(preferida);
                }
                else
                {
                    _context.Likes.Remove(l);
                    _context.SaveChanges();
                    return Ok(l);
                }    
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost("configuracao/restricao")]
        public ActionResult UpdateConfiguracaoPreferidas([FromQuery] int restricao)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                RestricoesAlimentares r = new RestricoesAlimentares();
                r.Utilizadorid = idUt;
                r.Ingredienteid = restricao;
                _context.RestricoesAlimentares.Add(r);
                _context.SaveChanges();
                return Ok(r);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("configuracao/restricao")]
        public ActionResult RemoveConfiguracaoExcluidas([FromQuery] int restricao)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                RestricoesAlimentares r = _context.RestricoesAlimentares.Find(idUt, restricao);
                if (r == null)
                {
                    return NotFound(restricao);
                }
                else
                {
                    _context.RestricoesAlimentares.Remove(r);
                    _context.SaveChanges();
                    return Ok(r);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("configuracao/excluida")]
        public ActionResult UpdateConfiguracaoExcluidas([FromQuery] int excluido)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                Dislikes l = new Dislikes();
                l.Utilizadorid = idUt;
                l.Tagid = excluido;
                _context.Dislikes.Add(l);
                _context.SaveChanges();
                return Ok(l);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("configuracao/excluida")]
        public ActionResult RemoveConfiguracaoRestricao([FromQuery] int excluido)
        {
            try
            {
                var sidut = ControllerContext.HttpContext.User.Identity.Name;
                int idUt = Int32.Parse(sidut);
                Dislikes l = _context.Dislikes.Find(idUt, excluido);
                if (l == null)
                {
                    return NotFound(excluido);
                }
                else
                {
                    _context.Dislikes.Remove(l);
                    _context.SaveChanges();
                    return Ok(l);
                }
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
                return Ok(new { o.Favorito, o.Rating, o.Blacklist});
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
                Opiniao o = _context.Opiniao.Find(idReceita,idUt);
                bool exists = true;
                if (o == null)
                {
                    exists = false;
                    o = new Opiniao();
                    o.Utilizadorid = idUt;
                    o.Receitaid = idReceita;
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
                    u = new UtilizadorPasso();
                    u.Utilizadorid = idUt;
                    u.Passoid = idPasso;
                    u.PassoReceitaid = idReceita;
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
                bool exists = true;
                if (u == null)
                {
                    exists = false;
                    u = new UtilizadorPasso();
                }
                u.Utilizadorid = idUt;
                u.PassoReceitaid = idReceita;
                u.Passoid = idPasso;
                u.Comentario = comentario;
                if(exists)
                    _context.UtilizadorPasso.Update(u);
                else
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
                Select(ce => new { numerodevezes = ce.Count(), lastDate = ce.Max(e => e.Data),
                                   ce.FirstOrDefault().Receita, ce.FirstOrDefault().Receita.Tempodepreparacao, tempoexecucao = ce.Min(e => e.DuracaoTotal)}).
                ToList();
            result.Sort( (e1,e2) => e2.lastDate.CompareTo(e1.lastDate));
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
                Include("Receita.ReceitaIngrediente.Ingrediente.Unidade").
                GroupBy(ri => ri.Ingredienteid).
                Select(cri => new { quantidade = cri.Sum(s => s.Quantidade) ,
                                    numerodeVezes = cri.Count(),
                                    ingrediente = cri.FirstOrDefault().Ingrediente,
                                    unidade = cri.FirstOrDefault().Ingrediente.Unidade.Nome
                }).
                ToList();
            result.Sort((i1, i2) => i2.numerodeVezes.CompareTo(i1.numerodeVezes));
            return Ok(result);
        }
        
        [HttpGet("tagsUsadas")]
        public ActionResult GetTagsUsadas()
        {
            var sidut = ControllerContext.HttpContext.User.Identity.Name;
            int idUt = Int32.Parse(sidut);
            var user = _context.Utilizador.Find(idUt);
            if (user == null)
            {
                return NotFound();
            }
            var result = _context.Execucao.
                Where(e => e.Utilizadorid == idUt).
                SelectMany(e => e.Receita.TagReceita).
                GroupBy(tr => tr.Tagid).
                Select(tu => new {
                    numerodeVezes = tu.Count(),
                    tag = tu.FirstOrDefault().Tag,
                }).
                ToList();
            result.Sort((t1, t2) => t2.numerodeVezes.CompareTo(t1.numerodeVezes));
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
                Include(r => r.Ingrediente).
                ThenInclude(i => i.Unidade).
                GroupBy(ri => ri.Ingredienteid).
                Select(cri => new {
                    quantidade = cri.Sum(s => s.Quantidade),
                    ingrediente = cri.FirstOrDefault().Ingrediente
                }).
                ToArray();
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
        /*
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
        */
        // POST: api/Utilizador/autenticar
        [HttpPost]
        [Route("autenticar")]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromForm] string email, [FromForm] string password)
        {
            if (email == null || password == null)
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
                Password = Myhelper.HashPassword(password)
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

        [HttpPost]
        [Route("registar")]
        [AllowAnonymous]
        public async Task<IActionResult> Registar([FromForm] Utilizador ut)
        {
            try
            {   
                Utilizador u = _context.Utilizador.Where(c => c.Email == ut.Email).FirstOrDefault();
                if (u != null)
                    return Redirect("/?email=" + ut.Email);
                ut.Id = 0;
                ut.Password = Myhelper.HashPassword(ut.Password);
                System.Diagnostics.Debug.WriteLine(ut.Password);
                _context.Utilizador.Add(ut);
                _context.SaveChanges();
                List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, ut.Id.ToString()),
            };
                ClaimsIdentity cIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(cIdentity);
                await HttpContext.SignInAsync(principal);

                //Request.HttpContext.
                return Redirect("/Home/Tutorial/");
            }
            catch
            {
                return Redirect("/?email=" + ut.Email);
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
