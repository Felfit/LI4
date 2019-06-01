﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweetChef.Models;
using SweetChef.ModelsNew;

namespace SweetChef.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SweetContext _context;

        public HomeController(SweetContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Home/Cozinhar/");
            }

            return View();
        }


        public IActionResult Cozinhar()
        {
            ViewData["tags"] = _context.Tag.Select(t => new { t.Id, Nome = t.Tag1 }).ToArray();
            ViewData["NomeUtilizador"] = GetNomeUtilizador();
            return View();
        }

        public IActionResult Configuracao()
        {
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        public IActionResult Editor(int id)
        {
            ViewData["NomeUtilizador"] = GetNomeUtilizador();
            if (_context.Receita.Find(id) != null)
                ViewData["ReceitaId"] = id;
            else
                ViewData["ReceitaId"] = "Unknown";

            return View();
        }

        public IActionResult Tutorial()
        {
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        public IActionResult EmentaSemanal()
        {
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        public IActionResult SobreCozinhados()
        {
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        public IActionResult Privacidade()
        {
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        public IActionResult ListaCompras()
        {
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

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
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

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
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        public IActionResult Utensilio(int id)
        {
            Utensilio utensilio = _context.Find<Utensilio>(id);
            if (utensilio == null)
            {
                return NotFound();
            }
            ViewData["Id"] = id;
            ViewData["Nome"] = utensilio.Nome;
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        public IActionResult PassoAPasso(int id, [FromQuery] bool? ultimoPasso, [FromQuery] int tempo)
        {
            Receita receita = _context.Find<Receita>(id);
            if(receita == null)
            {
                return NotFound();
            }
            ViewData["ReceitaId"] = id;
            ViewData["ReceitaNome"] = receita.Nome;
            ViewData["UltimoPasso"] = ultimoPasso;
            ViewData["tempo"] = tempo;
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        public IActionResult Feedback(int id, [FromQuery] int tempo)
        {
            Receita receita = _context.Find<Receita>(id);
            if (receita == null)
            {
                return NotFound();
            }
            ViewData["ReceitaId"] = id;
            ViewData["ReceitaNome"] = receita.Nome;
            ViewData["ReceitaDuracao"] = receita.Tempodeespera + receita.Tempodepreparacao;
            ViewData["tempo"] = tempo / 60000;
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["NomeUtilizador"] = GetNomeUtilizador();

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private String GetNomeUtilizador()
        {
            var sidut = ControllerContext.HttpContext.User.Identity.Name;
            int idUt = Int32.Parse(sidut);
            return _context.Utilizador.Where(p => p.Id == idUt).Select(u => u.Nome).First();
        }
    }
}
