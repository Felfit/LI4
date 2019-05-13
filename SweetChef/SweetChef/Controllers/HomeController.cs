using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SweetChef.Models;

namespace SweetChef.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Cozinhar()
        {
            ViewData["Message"] = "Bom trabalho Daniel.";

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
            //TODO: Perguntar à base de dados os dados da receita com este id
            ViewData["ReceitaId"] = id;

            return View();
        }

        public IActionResult PassoAPasso(int id)
        {
            //TODO: Perguntar à base de dados os dados da receita com este id
            ViewData["ReceitaId"] = id;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
