using DesafioUBC.Web.UI.Application.Extensions;
using DesafioUBC.Web.UI.Application.Interfaces;

namespace DesafioUBC.Web.Vue.UI.Configurations
{
    public static class ResponseAPIConfiguration
    {
        public static void UseResponseApi(this IApplicationBuilder applicationBuilder)
        {
            if (applicationBuilder == null) throw new ArgumentNullException(nameof(applicationBuilder));

            using (var scope = applicationBuilder.ApplicationServices.CreateScope())
            {
                ResponseExtensionAPI.Init(scope.ServiceProvider.GetService<IBaseService>());
            }
        }
    }
}