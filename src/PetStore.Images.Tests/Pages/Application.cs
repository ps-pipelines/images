using OpenQA.Selenium;

namespace PetStore.Images.Tests.Pages
{
    public class Application
    {
        public static Dashboard GoToDashBoard(IWebDriver driver)
        {
            var dash = new Dashboard(driver);
            driver.Navigate().GoToUrl(dash.Url);
            return dash;
        }
    }
}