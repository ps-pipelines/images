using System.Collections.Generic;

namespace PetStore.Images
{
    public class Store : IStore
    {
        public Store()
        {
            Files = new Dictionary<string, byte[]>();
        }

        public IDictionary<string, byte[]> Files { get; }
    }
}
