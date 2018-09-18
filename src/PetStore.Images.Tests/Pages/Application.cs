using OpenQA.Selenium;

namespace PetStore.Images.Tests.Pages
{
    using System.Threading;

    public class Application
    {
        public static Dashboard GoToDashBoard(IWebDriver driver)
        {
            var dash = new Dashboard(driver);
            driver.Navigate().GoToUrl(dash.Url);
            Thread.Sleep(1000);
            return dash;
        }
    }
}