using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.UI.Application.Responses;

namespace DesafioUBC.Web.UI.Application.Extensions
{
    public static class ResponseExtensionAPI
    {
        #region Properties

        private static IBaseService _baseService;

        #endregion

        #region Methods

        public static void Init(IBaseService baseService)
            => _baseService = baseService;

        public static async Task<ResponseAPIData<TEntity>> ToResponse<TEntity>(this HttpRequestMessage request) where TEntity : class
            => await _baseService.MontarResponse<TEntity>(request);

        public static async Task<ResponseAPIDataList<TEntity>> ToResponseList<TEntity>(this HttpRequestMessage request) where TEntity : class
            => await _baseService.MontarResponseList<TEntity>(request);

        public static async Task<ResponseAPIData<KeyValuePair<object, string>>> ToResponseRegraNegocio(this HttpRequestMessage request)
            => await _baseService.MontarResponseRegraNegocio(request);

        public static async Task<ResponseAPIDataListPaginations<TEntity>> ToResponseListPaginations<TEntity>(this HttpRequestMessage request) where TEntity : class
           => await _baseService.MontarResponseListPaginations<TEntity>(request);

        #endregion
    }
}
