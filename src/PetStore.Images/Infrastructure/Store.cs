using System.Collections.Generic;

namespace PetStore.Images.Infrastructure
{
    public static class Store
    {
        public static IDictionary<string, byte[]> Files { get; set; } = new Dictionary<string, byte[]>();
    }
}