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
            return Ok(_context.Tag.ToArray());
        }
    }
}