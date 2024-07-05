namespace DesafioUBC.Web.Vue.UI.Configurations
{
    public static class CommonConfiguration
    {
        //public static void AddAuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        //{
        //    if (services == null) throw new ArgumentNullException(nameof(services));

        //    //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    //    .AddCookie(opt =>
        //    //    {
        //    //        opt.LoginPath = new PathString("/Identity/Account/Login");
        //    //        opt.AccessDeniedPath = "/Error/403";
        //    //    });
        //}

        public static void AddAuthorizationConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAuthorization();
        }

        public static IApplicationBuilder UseCommonsConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment() || env.IsProduction() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityConfiguration();

         //   app.UseLoggingConfiguration(loggerFactory);

            app.UseGlobalizationConfig();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // Mapeando componentes Razor Pages (ex: Identity)
                endpoints.MapRazorPages();
            });

            return app;
        }
    }
}
