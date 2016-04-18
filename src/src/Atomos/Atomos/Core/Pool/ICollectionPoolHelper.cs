using System.Collections;

namespace Atomos
{
    public interface ICollectionPoolHelper<in TCollection> 
        where TCollection : ICollection
    {
        int GetCapacity(TCollection item);
    }
}