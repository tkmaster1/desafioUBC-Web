using Microsoft.Extensions.Configuration;

namespace DesafioUBC.Web.UI.Application.Services
{
    public class ApplicationsConfiguration
    {
        public Application Application { get; set; }

        public BaseUrl BaseUrl { get; set; }

        public Projeto PRJ { set; get; }
    }

    public class Application
    {
        public string Version { get; set; }

        public Application()
        {
            IConfiguration _configuration = new ConfigurationBuilder()
                                                .AddJsonFile("appsettings.json", true, true)
                                                .AddJsonFile($"appsettings.Development.json", true, true).Build();

            Version = _configuration["AppSettings:Application:Version"];
        }
    }

    public class BaseUrl
    {
        public string UrlApi { get; set; }
    }

    public class Projeto
    {
        public string APIBaseAddress { set; get; }
    }
}
