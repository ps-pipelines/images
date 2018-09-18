using OpenQA.Selenium;

namespace PetStore.Images.Tests.Pages
{
    using System.Threading;

    public class PetDetails
    {
        private readonly IWebDriver _driver;

        public PetDetails(IWebDriver driver)
        {
            _driver = driver;
        }

        public string Name => _driver.FindElement(By.CssSelector("h2")).Text;

        public void SetPetName(string name)
        {
            var element = _driver.FindElement(By.CssSelector("input"));
            element.Clear();
            element.SendKeys(name);
        }

        public Dashboard Save()
        {
            var element = _driver.FindElement(By.XPath("//button[text()='save']"));
            element.Click();
            Thread.Sleep(1000);
            return new Dashboard(_driver);
        }

    }
}