using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using PetStore.Images.Infrastructure;

namespace PetStore.Images.Tests.Infrastructure.WebClients
{
    public class IeWebClient : IWebClient
    {
        public IeWebClient()
        {
            var options = new InternetExplorerOptions();

            Env.Variables.TryGetValue("proxy", out var proxySetting);
            if (string.IsNullOrEmpty(proxySetting))
            {
                var proxy = new Proxy
                {
                    Kind = ProxyKind.Manual,
                    IsAutoDetect = false
                };
                proxy.HttpProxy = proxy.SslProxy = "http://proxy:32768";
                options.Proxy = proxy;
            }

            //need to place the exe in the following dir.
            Driver = new InternetExplorerDriver(@"C:\_dev\services\selenium", options);
            Driver.Manage().Window.FullScreen();
        }

        public void Dispose()
        {
            Driver?.Dispose();
        }

        public IWebDriver Driver { get; private set; }
    }
}