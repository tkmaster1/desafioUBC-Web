namespace DesafioUBC.Web.UI.Application.Extensions
{
    public static class StringExtensions
    {
        public static string ToFormat(this string mensagem, params object[] parametros)
        {
            return string.Format(mensagem, parametros);
        }
    }
}
