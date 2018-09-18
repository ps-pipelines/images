using System;
using System.Linq;
using NUnit.Framework;
using PetStore.Images.Tests.Infrastructure.WebClients;
using PetStore.Images.Tests.Pages;

namespace PetStore.Images.Tests
{
    /// <summary>
    /// this is a simple test(s) which will assert against the UI client
    ///
    /// this requires the site and client to be hosted in another process
    /// </summary>
    [Category("Ui")]
    [TestFixture]
    public class SimpleUiTest
    {
        private IWebClient _client;

        [SetUp]
        public void Setup()
        {
            _client = WebClientFactory.CreateWebClient();
        }

        [TearDown]
        public void Teardown()
        {
            _client.Dispose();
        }

        [Test]
        public void EditKitten()
        {
            var name = "kong" + DateTime.Now.ToString();

            var dashBoard = Application.GoToDashBoard(_client.Driver);
            var petDetails = dashBoard.ClickOnTopPet(2);
            petDetails.SetPetName(name);
            petDetails.Save();

            Assert.IsTrue(dashBoard.TopPets.Any(pet => pet == name));
        }

        [Test]
        public void SearchKitten()
        {
            var name = "because <script>alert('Injected!');</script> i can";

            var dashBoard = Application.GoToDashBoard(_client.Driver);
            var petDetails = dashBoard.ClickOnTopPet(2);
            petDetails.SetPetName(name);
            petDetails.Save();

            petDetails = dashBoard.Search("because");
            var result = petDetails.Name;
            Assert.AreEqual(result, name);

        }

    }
}