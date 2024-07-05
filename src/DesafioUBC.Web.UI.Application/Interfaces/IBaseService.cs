using DesafioUBC.Web.UI.Application.Responses;

namespace DesafioUBC.Web.UI.Application.Interfaces
{
    public interface IBaseService
    {
        string UrlBase { get; }

        string MontarParametros(string[] parametros);

        HttpClient MontarHttpClient(string mediaType = null);

        HttpRequestMessage Get(string url);

        HttpRequestMessage Delete(string url);

        HttpRequestMessage Post(string url, object parametros = null);

        HttpRequestMessage Put(string url, object parametros = null);

        HttpRequestMessage MontarRequest(string metodo, string url, object parametros = null);

        Task<ResponseAPIData<TEntity>> MontarResponse<TEntity>(HttpRequestMessage request) where TEntity : class;

        Task<ResponseAPIDataList<TEntity>> MontarResponseList<TEntity>(HttpRequestMessage request) where TEntity : class;

        Task<ResponseAPIDataListPaginations<TEntity>> MontarResponseListPaginations<TEntity>(HttpRequestMessage request) where TEntity : class;

        Task<ResponseAPIData<KeyValuePair<object, string>>> MontarResponseRegraNegocio(HttpRequestMessage request);
    }
}
