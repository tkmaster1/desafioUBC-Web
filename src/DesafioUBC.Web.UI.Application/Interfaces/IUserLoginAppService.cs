using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DesafioUBC.Web.UI.Application.Interfaces
{
    public interface IUserLoginAppService
    {
        string Name { get; }

        Guid GetUserId();

        string GetUserName();

        string GetUserEmail();

        string GetUserToken();

        string GetUserRefreshToken();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetClaimsIdentity();

        HttpContext GetHttpContext();
    }
}
