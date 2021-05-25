using Identity_2_2.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Identity_2_2.Extensions.PermissaoNecessaria;

namespace Identity_2_2.Config
{
    public static class DependencyInjectionConfig // #12 
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)  // #13 Criar método de extensão
        {
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            return services;
        }

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration configuration) // #14
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppIdentityContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("AppIdentityContextConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(Microsoft.AspNetCore.Identity.UI.UIFramework.Bootstrap4)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityContext>();

            return services;
        }
    }
}
