using System.Collections.Generic;

namespace PetStore.Images.Infrastructure
{
    public interface IStore
    {
        string Save(string name, byte[] asset);

        void UpdateName(string id, string name);

        Image Load(string id);

        IEnumerable<Image> Find(string filterByName = null);


    }
}
