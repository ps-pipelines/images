using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace PetStore.Images.Tests.Pages
{
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

            return new PetDetails(_driver);
        }

    }
}