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
    public class PassoController : Controller
    {
        private readonly SweetContext _context;

        public PassoController(SweetContext context)
        {
            _context = context;
        }

        // GET: Passo
        [HttpGet("{idReceita}/{idPasso}/getDuvidas")]
        public ActionResult getDuvidas(int idPasso, int idReceita)
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
    }
}