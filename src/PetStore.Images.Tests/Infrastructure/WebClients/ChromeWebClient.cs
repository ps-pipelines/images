using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PetStore.Images.Infrastructure;

namespace PetStore.Images.Tests.Infrastructure.WebClients
{
    public class ChromeWebClient : IWebClient
    {
        public ChromeWebClient()
        {
            var options = new ChromeOptions();

            Env.Variables.TryGetValue("proxy", out var proxySetting);
            if (string.IsNullOrEmpty(proxySetting))
            {
                var proxy = new Proxy
                {
                    Kind = ProxyKind.Manual,
                    IsAutoDetect = false
                };
                proxy.HttpProxy = proxy.SslProxy = "http://proxy:8081";
                options.Proxy = proxy;
            }

            options.AddArgument("ignore-certificate-errors");
            Driver = new ChromeDriver(options);
            Driver.Manage().Window.Maximize();
        }

        public void Dispose()
        {
            Driver?.Dispose();
        }

        public IWebDriver Driver { get; private set; }
    }
}