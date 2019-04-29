using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetChef.Models;

namespace SweetChef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        private readonly UtilizadorContext _context;

        // TODO: Corrigir erro
        
        public UtilizadorController(UtilizadorContext context)
        {
            _context = context;
        }

        // GET: api/Utilizador
        [HttpGet]
        public IEnumerable<Utilizador> Get()
        {
            return null;
            /*
            try
            {
                return _context.utilizadores.ToArray();
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            */
        }

        // GET: api/Utilizador/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Utilizador
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Utilizador/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
