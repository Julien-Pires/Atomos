using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos
{
    internal partial class KeyedPoolStorage<TItem, TKey> : IKeyedPoolStorage<TItem, TKey>
        where TItem : class
    {
        #region Fields

        private int _version;
        private List<KeyedPoolStorageItem> _availableItems = new List<KeyedPoolStorageItem>();
        private readonly HashSet<TItem> _availableItemsSet = new HashSet<TItem>();
        private readonly HashSet<TItem> _registeredItems = new HashSet<TItem>();

        #endregion

        #region Properties

        public TItem this[int index]
        {
            get { throw new System.NotImplementedException(); }
        }

        public int Count { get; }

        #endregion

        #region Enumerator

        public IEnumerator<TItem> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Storage Management

        public void SetCapacity(int capacity)
        {
            throw new System.NotImplementedException();
        }

        public void ResetItems()
        {
            throw new System.NotImplementedException();
        }

        public void DestroyItems()
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Pooling

        private KeyedPoolStorageItem FindItemContainer(TKey key)
        {
        }

        public void Register(TItem item)
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered(TItem item) => _registeredItems.Contains(item);

        public bool IsAvailable(TItem item) => _availableItemsSet.Contains(item);

        public TItem Get(TKey key)
        {
            throw new NotImplementedException();
        }

        TItem IPoolStorage<TItem>.Get()
        {
            throw new NotSupportedException("Keyed storage can't retrieve item without key");
        }

        public void Set(TItem item)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}