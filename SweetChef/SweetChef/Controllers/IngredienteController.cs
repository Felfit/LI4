using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SweetChef.ModelsNew;

namespace SweetChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPost]
        public ActionResult PostIngrediente([FromForm]Ingrediente ing)
        {
            _context.Ingrediente.Add(ing);
            _context.SaveChanges();
            return Redirect(Request.Host.Value);
        }

        [HttpPut]
        public ActionResult UpdateIngrediente([FromForm]Ingrediente ing)
        {
            _context.Ingrediente.Update(ing);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("unidade")]
        public ActionResult GetUnidade()
        {
            return Ok(_context.Unidade.ToArray());
        }

        [HttpPost("unidade")]
        public ActionResult PostUnidade([FromForm]Unidade un)
        {
            _context.Unidade.Add(un);
            _context.SaveChanges();
            return Ok();
        }
    }
}