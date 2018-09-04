using System;
using OpenQA.Selenium;

namespace PetStore.Images.Tests.Infrastructure.WebClients
{
    public interface IWebClient: IDisposable
    {
        IWebDriver Driver { get; }
    }
}