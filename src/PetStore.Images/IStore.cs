using System.Collections.Generic;

namespace PetStore.Images
{
    public interface IStore
    {
        IDictionary<string, byte[]> Files { get; }
    }
}
