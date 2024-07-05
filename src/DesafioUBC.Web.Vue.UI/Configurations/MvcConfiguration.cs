using DesafioUBC.Web.UI.Application.BaseService;
using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.UI.Application.Notifications;
using DesafioUBC.Web.Vue.UI.Configurations.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO.Compression;

namespace DesafioUBC.Web.Vue.UI.Configurations
{
    public static class MvcConfiguration
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddControllers(x =>
                {
                    x.Filters.Add(typeof(CustomActionFilterConfig));
                }
            );

            services.AddControllersWithViews();

            // Adicionando suporte a componentes Razor (ex: Telas do Identity)
            services.AddRazorPages();

            // Comentar esta parte quando quiser utilizar em Postman
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = false;
            });

            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "X-XSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });

            var _appConfig = configuration.GetSection("AppSettings").Get<ApplicationsConfiguration>();

            services.AddSingleton(_appConfig);
            services.Configure<ApplicationsConfiguration>(configuration.GetSection("AppSettings"));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddHttpContextAccessor();

            //aumenta o limite do corpo da requisição multipart 
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = long.MaxValue;
                options.ValueLengthLimit = int.MaxValue;
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddHttpClient();

            // Comentar esta parte quando quiser utilizar em Postman - esta também
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                options.EnableEndpointRouting = false;
            });

            var provider = services.BuildServiceProvider();

            services.AddTransient<IBaseService>(c => new BaseService(provider.GetService<IHttpContextAccessor>(),
                                                                     provider.GetService<IHttpClientFactory>(),
                                                                     _appConfig,
                                                                     provider.GetService<INotificationHandler<Notification>>()));

            services.AddCors();

            return services;
        }
    }
}
