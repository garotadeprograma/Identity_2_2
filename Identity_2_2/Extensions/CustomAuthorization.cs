using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Security.Claims;

namespace Identity_2_2.Extensions
{
    public class CustomAuthorization // #3 Criar classe de autorização
    {
        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue) // #4 Criar método que valida o claim do usuário
        {
            return context.User.Identity.IsAuthenticated &&
                   context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }

        public class RequisitoClaimFilter : IAuthorizationFilter // #5 Criar filtro para ver se o usuário pode ou não entrar
        {
            private readonly Claim _claim; // #6 Criar o claim

            public RequisitoClaimFilter(Claim claim) // #7 Injetar o claim
            {
                _claim = claim;
            }

            public class ClaimsAuthorizeAttribute : TypeFilterAttribute // #9 Chama o método de autorização
            {
                public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
                {
                    Arguments = new object[] { new Claim(claimName, claimValue) };
                }
            }

            public void OnAuthorization(AuthorizationFilterContext context) // #8 Permitir ou barrar o acesso
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
                }

                if (!ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
                {
                    context.Result = new StatusCodeResult(403);
                }
            }
        }
    }
}
