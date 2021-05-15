using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity_2_2.Extensions
{
    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string Permissao { get; }

        public PermissaoNecessaria(string permissao)
        {
            Permissao = permissao;
        }

        public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
        {
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext contexto, PermissaoNecessaria requisito)
            {
                if(contexto.User.HasClaim(c => c.Type == "Permissoes" && c.Value.Contains(requisito.Permissao)))
                { 
                contexto.Succeed(requisito);
                }

                return Task.CompletedTask;
            }
        }
    }
}
