using System.IO;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using PetStore.Images.Tests.Infrastructure.Servers;

namespace PetStore.Images.Tests
{
    /// <summary>
    /// this is a simple test(s) which will assert against the API
    ///
    /// this can be run in memory as well as remotely
    /// </summary>
    [Category("Api")]
    [TestFixture]
    public class SimpleApiTest
    {

        HttpClient httpClient;//_server.CreateClient();
        IServer server;

        [SetUp]
        public void SetupDependencies()
        {
            server = ServerFactory.CreateServer();
            httpClient = server.Client;
        }

        [TearDown]
        public void TearDownDependencies()
        {
            server?.Dispose();
        }


        [Test]
        public void When_image_is_uploaded_Then_it_can_be_retrieved()
        {
            //arrange
            var path = Path.Combine(Directory.GetCurrentDirectory(), "kitten.jpeg");
            string imageId = null;

            using (var file = File.Open(path, FileMode.Open))
            {
                HttpContent fileStreamContent = new StreamContent(file);
                fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "files", FileName = "kitten.jpeg" };
                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("images/jpeg");

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(fileStreamContent);
                    var response = httpClient.PostAsync("/api/images", formData).Result;

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                    imageId = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.IsSuccessStatusCode);
                }
            }


            //action
            var imageResponse = httpClient.GetAsync($"/api/images/content/{imageId}").Result;
            var imageByteArray = imageResponse.Content.ReadAsByteArrayAsync().Result;

            //assert
            var expected = File.ReadAllBytesAsync(path).Result;

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], imageByteArray[i]);
            }

        }



        [Test]
        public void When_image_is_uploaded_Then_it_can_be_retrieved_2()
        {
            //arrange
            var path = Path.Combine(Directory.GetCurrentDirectory(), "kitten.jpeg");
            string imageId = null;

            using (var file = File.Open(path, FileMode.Open))
            {
                HttpContent fileStreamContent = new StreamContent(file);
                fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "files", FileName = "kitten.jpeg" };
                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("images/jpeg");

                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(fileStreamContent);
                    var response = httpClient.PostAsync("/api/images", formData).Result;

                    Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

                    imageId = response.Content.ReadAsStringAsync().Result;
                    Assert.IsTrue(response.IsSuccessStatusCode);
                }
            }


            //action
            var imageResponse = httpClient.GetAsync($"/api/images/?id={imageId}").Result;
            var content = imageResponse.Content.ReadAsStringAsync().Result;

            //assert - trying to show a hackable root
            Assert.IsTrue(!string.IsNullOrEmpty(content));

        }

    }
}