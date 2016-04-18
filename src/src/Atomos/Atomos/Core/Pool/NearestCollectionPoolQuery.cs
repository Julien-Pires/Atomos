using System;
using System.Collections;

namespace Atomos
{
    internal sealed class NearestCollectionPoolQuery<TCollection> : IPoolStorageQuery<TCollection, int?>
        where TCollection : class, ICollection
    {
        #region Fields

        private readonly int _defaultCapacity;

        #endregion

        #region Constructor

        public NearestCollectionPoolQuery(int defaultCapacity)
        {
            _defaultCapacity = defaultCapacity;
        } 

        #endregion

        #region Insert

        public void Insert(IPoolStorage<TCollection> storage, TCollection item, int? parameter)
        {
            storage.Set(item);
        }

        #endregion

        #region Search

        public TCollection Search(IPoolStorage<TCollection> storage, int? parameter)
        {
            KeyedPoolStorage<TCollection, int> keyedStorage = (KeyedPoolStorage<TCollection, int>) storage;
            int index = keyedStorage.FindKeyIndex(parameter ?? _defaultCapacity);
            if (index < 0)
            {
                index = ~index;
                if (index >= keyedStorage.KeyCount)
                    index = -1;
            }

            return (index > -1) ? keyedStorage.GetByIndex(index) : null;
        }

        #endregion
    }
}