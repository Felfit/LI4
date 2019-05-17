using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SweetChef.Models;
using SweetChef.ModelsNew;

namespace SweetChef.Controllers
{
    public class HomeController : Controller
    {
        private readonly SweetContext _context;

        public HomeController(SweetContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Cozinhar()
        {
            return View();
        }

        public IActionResult EmentaSemanal()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult SobreCozinhados()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacidade()
        {
            return View();
        }
        
        public IActionResult Receita(int id)
        {
            Receita receita = _context.Find<Receita>(id);
            if (receita == null)
            {
                return NotFound();
            }
            ViewData["ReceitaId"] = id;
            ViewData["ReceitaNome"] = receita.Nome;

            return View();
        }

        public IActionResult Ingrediente(int id)
        {
            Ingrediente ingrediente = _context.Find<Ingrediente>(id);
            if (ingrediente == null)
            {
                return NotFound();
            }
            ViewData["Id"] = id;
            ViewData["Nome"] = ingrediente.Nome;

            return View();
        }

        public IActionResult PassoAPasso(int id)
        {
            Receita receita = _context.Find<Receita>(id);
            if(receita == null)
            {
                return NotFound();
            }
            ViewData["ReceitaId"] = id;
            ViewData["ReceitaNome"] = receita.Nome;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
