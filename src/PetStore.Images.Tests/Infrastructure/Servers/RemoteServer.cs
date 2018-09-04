using System;
using System.Net;
using System.Net.Http;
using PetStore.Images.Infrastructure;

namespace PetStore.Images.Tests.Infrastructure.Servers
{
    public class RemoteServer : IServer
    {
        private HttpClient _httpClient;

        public RemoteServer()
        {
            Console.WriteLine("setting up the RemoteServer");

            string proxy;
            Env.Variables.TryGetValue("proxy", out proxy);

            if (string.IsNullOrEmpty(proxy))
            {
                _httpClient = new HttpClient();
            }
            else
            {
                var handler = new HttpClientHandler()
                {
                    Proxy = new WebProxy(new Uri("http://proxy:32768"), false),
                    UseProxy = true
                };

                _httpClient = new HttpClient(handler);
            }

            var portNumber = Environment.GetEnvironmentVariable("Port");
            var url = "http://sut" + (string.IsNullOrEmpty(portNumber) ? "" : $":{portNumber}");
            
            _httpClient.BaseAddress = new Uri(url);
        }


        public HttpClient Client => _httpClient;

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}