using PetStore.Images.Infrastructure;

namespace PetStore.Images.Tests.Infrastructure.WebClients
{

    public class WebClientFactory
    {
        public static IWebClient CreateWebClient()
        {
            Env.Variables.TryGetValue("driver", out var driver);
            var webClient = string.IsNullOrWhiteSpace(driver) || driver == "chrome"
                ? (IWebClient) new ChromeWebClient()
                : (IWebClient) new IeWebClient();

            return webClient;
        }
    }
}
