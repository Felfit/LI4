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
    public class TagController : ControllerBase
    {

        private readonly SweetContext _context;

        public TagController(SweetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Tags()
        {
            var t = _context.Tag.Select(ts => new { ts.Id, nome = ts.Tag1}).ToArray();
            if(t.Length == 0)
                return Ok("[]");
            return Ok(t);
        }
    }
}