using DesafioUBC.Web.UI.Application.Interfaces;
using DesafioUBC.Web.UI.Application.Responses;
using DesafioUBC.Web.UI.Application.Services;
using DesafioUBC.Web.UI.Application.Services.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using static DesafioUBC.Web.UI.Application.Services.Enum.EnumConfigApp;

namespace DesafioUBC.Web.UI.Application.BaseService
{
    public class BaseService : IBaseService
    {
        #region Properties

        private bool _disposed = false;
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        private readonly IHttpContextAccessor _httpContextHttp;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ApplicationsConfiguration _appConfig;
        private readonly NotificationHandler _notifications;

        public string UrlBase { get; protected set; }

        #endregion

        #region Constructor

        public BaseService(IHttpContextAccessor httpContextHttp,
                           IHttpClientFactory httpClientFactory,
                           ApplicationsConfiguration appConfig,
                           INotificationHandler<Notification> notifications)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextHttp = httpContextHttp;
            _appConfig = appConfig;
            UrlBase = _appConfig.BaseUrl.UrlApi;
            _notifications = (NotificationHandler)notifications;
        }

        #endregion

        #region Methods RequestMessages

        public HttpRequestMessage Get(string url)
        {
            var request = RetornaRequest(TipoMetodo.Get);

            request.RequestUri = new Uri(url);

            return request;
        }

        public HttpRequestMessage Post(string url, object parametros = null)
        {
            var request = RetornaRequest(TipoMetodo.Post, parametros);

            request.RequestUri = new Uri(url);

            return request;
        }

        public HttpRequestMessage Put(string url, object parametros = null)
        {
            var request = RetornaRequest(TipoMetodo.Put, parametros);

            request.RequestUri = new Uri(url);

            return request;
        }

        public HttpRequestMessage Delete(string url)
        {
            var request = RetornaRequest(TipoMetodo.Delete);

            request.RequestUri = new Uri(url);

            return request;
        }

        #endregion

        #region Methods Responses

        public async Task<ResponseAPIData<TEntity>> MontarResponse<TEntity>(HttpRequestMessage request) where TEntity : class
        {
            using HttpResponseMessage response = await MontarRequest(request);

            string responseBody = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<ResponseAPIData<TEntity>>(responseBody);

            if (!response.IsSuccessStatusCode)
            {
                await _notifications.Handle(new Notification(response?.StatusCode.ToString() ?? "500",
                                                                       response?.StatusCode.ToString() == "Unauthorized"
                                                                           ? "Unauthorized"
                                                                           : retorno?.Errors?.ToString() ?? "" + " : " + request.RequestUri.AbsolutePath));

            }

            return retorno;
        }

        public async Task<ResponseAPIDataList<TEntity>> MontarResponseList<TEntity>(HttpRequestMessage request) where TEntity : class
        {
            var client = MontarHttpClient();

            using (HttpResponseMessage response = await client.SendAsync(request))
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<ResponseAPIDataList<TEntity>>(responseBody);

                string mensagemRetorno = string.Empty;
                if (!response.IsSuccessStatusCode)
                {
                    if (response?.StatusCode.ToString() == "Unauthorized")
                    {
                        mensagemRetorno = "Unauthorized";
                    }
                    else if (!string.IsNullOrEmpty(retorno?.Errors?.ToString()))
                    {
                        mensagemRetorno = retorno?.Errors?.ToString();
                    }
                    else
                    {
                        mensagemRetorno = string.Empty + " : " + request.RequestUri.AbsolutePath;
                    }

                    await _notifications.Handle(new Notification(response?.StatusCode.ToString() ?? "500", mensagemRetorno));
                }

                return retorno;
            }
        }

        public async Task<ResponseAPIDataListPaginations<TEntity>> MontarResponseListPaginations<TEntity>(HttpRequestMessage request) where TEntity : class
        {
            using HttpResponseMessage response = await MontarRequest(request);

            string responseBody = await response.Content.ReadAsStringAsync();
            var retorno = JsonConvert.DeserializeObject<ResponseAPIDataListPaginations<TEntity>>(responseBody);

            string mensagemRetorno = string.Empty;
            if (!response.IsSuccessStatusCode)
            {
                await _notifications.Handle(new Notification(response?.StatusCode.ToString() ?? "500",
                                                                       response?.StatusCode.ToString() == "Unauthorized"
                                                                           ? "Unauthorized"
                                                                           : retorno?.Errors?.ToString() ?? "" + " : " + request.RequestUri.AbsolutePath));

            }

            return retorno;
        }

        public async Task<ResponseAPIData<KeyValuePair<object, string>>> MontarResponseRegraNegocio(HttpRequestMessage request)
        {
            var client = MontarHttpClient();

            using (HttpResponseMessage response = await client.SendAsync(request))
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<ResponseAPIData<KeyValuePair<object, string>>>(responseBody);

                if (!response.IsSuccessStatusCode)
                {
                    await _notifications.Handle(new Notification(response?.StatusCode.ToString() ?? "500",
                                                                   response?.StatusCode.ToString() == "Unauthorized"
                                                                        ? "Unauthorized"
                                                                        : retorno?.Errors?.ToString() ?? string.Empty + " : " + request.RequestUri.AbsolutePath));
                }

                return retorno;
            }
        }

        #endregion

        #region Methods Commons

        public HttpClient MontarHttpClient(string mediaType = null)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();

            if (!string.IsNullOrEmpty(mediaType) && mediaType == "multipart")
            {
                client.DefaultRequestHeaders.TransferEncodingChunked = true;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            }
            else
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.BaseAddress = new Uri(UrlBase);

            if (string.IsNullOrEmpty(mediaType))
            {
                // Associar o token aos headers do objeto do tipo HttpClient
                var token = _httpContextHttp.HttpContext.User.GetUserToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }

        public string MontarParametros(string[] parametros)
        {
            var retorno = parametros == null ? string.Empty
                        : parametros.Length > 1
                            ? ("?" + string.Join("&", parametros))
                            : parametros.Length == 1
                        ? (parametros[0]) : string.Empty;

            return retorno;
        }

        public HttpRequestMessage MontarRequest(string metodo, string url, object? parametros = null)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);

            // Associar o token aos headers do objeto do tipo HttpClient
            var token = _httpContextHttp.HttpContext.User.GetUserToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            switch (metodo)
            {
                case "GET":
                    request.Method = HttpMethod.Get;
                    return request;

                case "PUT":
                    request.Method = HttpMethod.Put;
                    request.Content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");
                    return request;

                case "POST":
                    request.Method = HttpMethod.Post;
                    request.Content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");
                    return request;

                case "DELETE":
                    request.Method = HttpMethod.Delete;
                    return request;

                default:
                    break;
            }

            return request;
        }

        #endregion

        #region Methods Disposes

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _safeHandle?.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Methods Privates

        private HttpRequestMessage RetornaRequest(TipoMetodo tipoMetodo, object parametros = null)
        {
            var request = new HttpRequestMessage();

            switch (tipoMetodo)
            {
                case TipoMetodo.Get:
                    request.Method = HttpMethod.Get;
                    break;
                case TipoMetodo.Post:
                    request.Method = HttpMethod.Post;
                    request.Content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");

                    break;
                case TipoMetodo.Put:
                    request.Method = HttpMethod.Put;
                    request.Content = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");

                    break;
                case TipoMetodo.Delete:
                    request.Method = HttpMethod.Delete;
                    break;
                default:
                    break;
            }

            return request;
        }

        private async Task<HttpResponseMessage> MontarRequest(HttpRequestMessage request)
        {
            var client = MontarHttpClient();

            var response = await client.SendAsync(request);

            return response;
        }

        #endregion
    }
}
