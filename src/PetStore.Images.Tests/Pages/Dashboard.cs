using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace PetStore.Images.Tests.Pages
{
    using System.Threading;

    public class Dashboard
    {
        private readonly IWebDriver _driver;

        public Dashboard(IWebDriver driver)
        {
            _driver = driver;
        }

        public string Url => "http://sut:4200/";


        public IEnumerable<string> TopPets
        {

            get
            {
                return _driver.FindElements(By.CssSelector(".topPet > div > h4")).Select(x => x.Text);
            }
        }


        public PetDetails ClickOnTopPet(int position)
        {
            var elements = _driver.FindElements(By.CssSelector(".topPet"));
            if (!elements.Any()) return null;
            var element = elements.ElementAt(position);
            if (element == null) return null;
            element?.Click();
            Thread.Sleep(1000); 
            return new PetDetails(_driver);
        }


        public PetDetails Search(string name)
        {
            var element = _driver.FindElement(By.Id("search-box"));
            element.SendKeys(name);
            Thread.Sleep(100);

            var result = _driver.FindElements(By.ClassName("search-result")).FirstOrDefault();
            Thread.Sleep(500);
            result.FindElement(By.CssSelector("a")).Click();

            Thread.Sleep(1000);
            return new PetDetails(_driver);
        }

    }
}