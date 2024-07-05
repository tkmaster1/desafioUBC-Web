using DesafioUBC.Web.UI.Application.DTOs;
using DesafioUBC.Web.UI.Application.Responses;

namespace DesafioUBC.Web.UI.Application.Interfaces
{
    public interface ILoginRegisterUserAppService : IDisposable
    {
        /// Método que realiza o Login do usuário na aplicação
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        Task<ResponseAPIData<object>> LoginAPI(LoginUserRequestDTO req);
    }
}
