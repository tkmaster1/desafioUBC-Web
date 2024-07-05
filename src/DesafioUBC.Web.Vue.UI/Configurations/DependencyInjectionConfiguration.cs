using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.UI.Application.Services;
using DesafioUBC.Web.UI.Application.Services.Notifications;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace DesafioUBC.Web.Vue.UI.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Lifestyle.Transient => Uma instância para cada solicitação
            // Lifestyle.Singleton => Uma instância única para a classe (para servidor)
            // Lifestyle.Scoped => Uma instância única para o request

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();

            services.AddScoped<IUserLoginAppService, UserLoginAppService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
