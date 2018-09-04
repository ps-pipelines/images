using OpenQA.Selenium;

namespace PetStore.Images.Tests.Pages
{
    public class PetDetails
    {
        private readonly IWebDriver _driver;

        public PetDetails(IWebDriver driver)
        {
            _driver = driver;
        }

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
            return new Dashboard(_driver);
        }

    }
}