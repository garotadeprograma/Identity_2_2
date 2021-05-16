using Identity_2_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Identity_2_2.Extensions.CustomAuthorization.RequisitoClaimFilter;

namespace Identity_2_2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AreaRestrita()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult AreaRestritaClaim()
        {
            return View();
        }

        [Authorize(Policy = "PodeLer")]
        public IActionResult AreaRestritaPodeLer()
        {
            return View();
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult AreaRestritaPodeEscrever()
        {
            return View();
        }

        [ClaimsAuthorize("Custom", "Ler")] // #2 Criar o authorize
        public IActionResult AreaRestritaCustom() // #1 Criar um método
        {
            return View("AreaRestrita");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
