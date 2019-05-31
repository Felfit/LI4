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
    public class DuvidaController : Controller
    {
        private readonly SweetContext _context;

        public DuvidaController(SweetContext context)
        {
            _context = context;
        }

        // GET: Passo
        [HttpGet("{idReceita}/{idPasso}")]
        public ActionResult getDuvidasFromPasso(int idPasso, int idReceita)
        {
            try
            {
                var passo = _context.Passo.
                            Include(p => p.PassoDúvida).
                            ThenInclude(pd => pd.Dúvida).
                            Where(p => p.Receitaid == idReceita && p.Numero == idPasso).
                            FirstOrDefault();
                if (passo == null)
                    return NotFound();
                var result = passo.PassoDúvida.Select(pd => new { pd.Questao, pd.Dúvida }).ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // api/duvida
        [HttpPost("duvidaPasso")]
        public ActionResult addPassoDuvida([FromForm] PassoDúvida pd)
        {
            try
            {
                _context.PassoDúvida.Add(pd);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // api/duvida
        [HttpPut("duvidaPasso")]
        public ActionResult updatePassoDuvida([FromForm] PassoDúvida pd)
        {
            try
            {
                _context.PassoDúvida.Update(pd);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // api/duvida
        [HttpPost]
        public ActionResult addDuvida([FromForm] Duvida d)
        {
            try
            {
                _context.Duvida.Add(d);
                _context.SaveChanges();
                return Ok(d);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        // api/duvida
        [HttpPut]
        public ActionResult updateDuvida([FromForm] Duvida d)
        {
            try
            {
                _context.Duvida.Update(d);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}