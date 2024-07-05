using DesafioUBC.Web.UI.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace DesafioUBC.Web.UI.Application.Services
{
    public class UserLoginAppService : IUserLoginAppService
    {
        #region Properties

        private readonly IHttpContextAccessor _accessor;

        #endregion

        #region Construtor

        public UserLoginAppService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        #endregion

        #region Methods

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid GetUserId()
        {
            return IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string GetUserName()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserName() : "";
        }

        public string GetUserEmail()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public string GetUserToken()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : "";
        }

        public string GetUserRefreshToken()
        {
            return IsAuthenticated() ? _accessor.HttpContext.User.GetUserRefreshToken() : "";
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public HttpContext GetHttpContext()
        {
            return _accessor.HttpContext;
        }

        #endregion
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("nameid");
            return claim?.Value;
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("name");
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("email");
            return claim?.Value;
        }

        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }

        public static string GetUserRefreshToken(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentException(nameof(principal));

            var claim = principal.FindFirst("RefreshToken");
            return claim?.Value;
        }
    }
}
