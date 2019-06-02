using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetChef.ModelsNew;

namespace SweetChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UtensilioController : ControllerBase
    {
        private readonly SweetContext _context;

        public UtensilioController(SweetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetUtensilios() => Ok(_context.Utensilio.ToList());
       

        [HttpPost]
        public ActionResult PostUtensilio([FromQuery]Utensilio u){
            if (_context.Utensilio.Where(us => us.Nome == u.Nome).Count() > 0)
                return BadRequest();
            _context.Utensilio.Add(u);
            _context.SaveChanges();
            return Ok();
        }
        [HttpPost]
        public ActionResult PutUtensilio([FromQuery]Utensilio u)
        {
            _context.Utensilio.Update(u);
            _context.SaveChanges();
            return Ok();
        }
    }
}