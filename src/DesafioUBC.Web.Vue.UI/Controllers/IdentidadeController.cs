using AutoMapper;
using DesafioUBC.Web.UI.Application.DTOs;
using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.UI.Application.Responses;
using DesafioUBC.Web.Vue.UI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DesafioUBC.Web.Vue.UI.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[Action]")]
    public class IdentidadeController : MainController
    {
        #region Properties

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserLoginAppService _userLoginAppService;

        protected Guid UsuarioId { get; set; }

        protected bool UsuarioAutenticado { get; set; }

        #endregion

        #region Constructor

        public IdentidadeController(IUnitOfWork unitOfWork,
                                    IMapper mapper,
                                    IAuthenticationService authenticationService,
                                    IUserLoginAppService userLoginAppService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticationService = authenticationService;
            _userLoginAppService = userLoginAppService;

            if (_userLoginAppService.IsAuthenticated())
            {
                UsuarioId = _userLoginAppService.GetUserId();
                UsuarioAutenticado = true;
            }
        }

        #endregion

        #region Views

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        #endregion

        #region Methods

        [HttpPost]
        public async Task<IActionResult> SaveRegisterAndLogin([FromBody] LoginUserViewModel usuarioLogin, string returnUrl = null)
        {
            string Mensagem;
            bool Success;
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid) return Json(new { success = false, mensagem = ModelState });

            var usuarioDomain = _mapper.Map<LoginUserViewModel, LoginUserRequestDTO>(usuarioLogin);
            var result = await _unitOfWork.LoginRegisterUserApp.LoginAPI(usuarioDomain);

            if (!result.Success)
            {
                var retorno = JsonConvert.DeserializeObject<Erros>(result.Errors.ToString());

                Success = false;
                Mensagem = "* " + retorno.Value;
            }
            else
            {
                var resposta = JsonConvert.DeserializeObject<ResponseLoginUserIdentity>(result.Data.ToString());
                if (resposta.AccessToken != null)
                    await LoginPerform(resposta);
                else
                {
                    ViewData["Message"] = resposta.Message;
                    return View(usuarioLogin);
                }

                Success = true;
                Mensagem = "Inclusão realizada com sucesso";
            }

            return Json(new { success = Success, mensagem = Mensagem });
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticationService.SignOutAsync(
                  _userLoginAppService.GetHttpContext(),
                 CookieAuthenticationDefaults.AuthenticationScheme,
                 null);

            return RedirectToAction("Login", "Identidade");
        }

        #endregion

        #region Methods Private

        private async Task LoginPerform(ResponseLoginUserIdentity resposta)
        {
            var token = ObterTokenFormatado(resposta.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", resposta.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            authProperties.StoreTokens(new List<AuthenticationToken>() { new AuthenticationToken() { Name = "JWT", Value = resposta.AccessToken } });

            await _authenticationService.SignInAsync(
                _userLoginAppService.GetHttpContext(),
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
            => new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;

        #endregion
    }
}