using PetStore.Images.Infrastructure;

namespace PetStore.Images.Tests.Infrastructure.Servers
{
    public class ServerFactory
    {
        public static IServer CreateServer()
        {
            string isCi;
            Env.Variables.TryGetValue("IsCi", out isCi);

            var server = !string.IsNullOrEmpty(isCi)
                ? (IServer) new InMemoryServer()
                : (IServer) new RemoteServer();

            return server;
        }
    }
}
