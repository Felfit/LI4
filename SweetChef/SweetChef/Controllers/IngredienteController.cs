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
    public class IngredienteController : ControllerBase
    {
        private readonly SweetContext _context;

        public IngredienteController(SweetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Ingredientes()
        {
            var t = _context.Ingrediente.Include(i => i.Unidade).ToArray();
            if (t.Length == 0)
                return Ok("[]");
            return Ok(t);
        }
    }
}