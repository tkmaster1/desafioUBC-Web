using DesafioUBC.Web.UI.Application.DTOs;
using DesafioUBC.Web.UI.Application.Extensions;
using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.UI.Application.Responses;

namespace DesafioUBC.Web.UI.Application.Services
{
    public class LoginRegisterUserAppService : ILoginRegisterUserAppService
    {
        #region Properties

        private readonly IBaseService _baseService;

        #endregion

        #region Constructor

        public LoginRegisterUserAppService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        #endregion

        #region Methods Publics

        public async Task<ResponseAPIData<object>> LoginAPI(LoginUserRequestDTO req)
        {
            string url = $"{_baseService.UrlBase}/Auth/login/";

            return await _baseService.Post(url, req).ToResponse<object>();
        }

        public void Dispose() => GC.SuppressFinalize(this);

        #endregion
    }
}
