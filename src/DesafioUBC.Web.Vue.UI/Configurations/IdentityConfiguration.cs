using Microsoft.AspNetCore.Authentication.Cookies;

namespace DesafioUBC.Web.Vue.UI.Configurations
{
    public static class IdentityConfiguration
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.SlidingExpiration = true;
                    options.LoginPath = "/Identidade/Login"; //"/Login";
                    options.AccessDeniedPath = "/erro/403";
                });

            return services;
        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            // Autenticacao e autorização (Identity)
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
