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
            throw new Exception("Erro");
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

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Mensagem = ("Ocorreu um erro em nosso servidor");
                modelError.Titulo = ("Erro 500");
                modelError.ErroCode = id;
            }
            else if(id == 403)
            {
                modelError.Mensagem = ("Você não tem permissão para acessar esta página");
                modelError.Titulo = ("Acesso Negado");
                modelError.ErroCode = id;
            }
            else if(id == 404)
            {
                modelError.Mensagem = ("Página não localizada");
                modelError.Titulo = ("Erro 404");
                modelError.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }
            return View("Error", modelError);
        }
    }
}
