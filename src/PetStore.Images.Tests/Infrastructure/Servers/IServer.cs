using System;
using System.Net.Http;

namespace PetStore.Images.Tests.Infrastructure.Servers
{
    public interface IServer : IDisposable
    {
        HttpClient Client { get; }
    }
}