using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace PetStore.Images.Tests.Infrastructure.Servers
{
    public class InMemoryServer : IServer
    {
        private readonly TestServer _server;
        private readonly HttpClient _httpClient;

        public InMemoryServer()
        {
            Console.WriteLine("setting up the InMemoryServer");
            _server = new Microsoft.AspNetCore.TestHost.TestServer(
                new WebHostBuilder()
                    .UseStartup<Startup>()
                    .UseUrls($"http://*:5000")
            );

            _httpClient = _server.CreateClient();
        }


        public HttpClient Client => _httpClient;

        public void Dispose()
        {
            _httpClient.Dispose();
            _server.Dispose();
        }
    }
}