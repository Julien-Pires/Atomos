using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos
{
    internal partial class KeyedPoolStorage<TItem, TKey> : IPoolStorage<TItem>
        where TItem : class
    {
        #region Fields

        private int _version;
        private List<KeyedPoolStorageItem> _availableItems = new List<KeyedPoolStorageItem>();
        private readonly Dictionary<TItem, TKey> _availableItemsSet = new Dictionary<TItem, TKey>();
        private readonly Dictionary<TItem, TKey> _registeredItems = new Dictionary<TItem, TKey>();

        #endregion

        #region Properties

        public KeyedPoolStorageItem this[int index] => _availableItems[index];

        public int Count => _availableItems.Count;

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

        public void DestroyItems()
        {
            _availableItems.Clear();
            _availableItemsSet.Clear();
            _registeredItems.Clear();

            _version++;
        }

        public void ResetItems()
        {
            _registeredItems
            _availableItemsSet.UnionWith(_registeredItems);
            _availableItemsSet.UnionWith(_registeredItems);
            _availableItems.Clear();
            _availableItems.AddRange(_availableItemsSet);

            _version++;
        }

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            List<KeyedPoolStorageItem> availableItems = new List<KeyedPoolStorageItem>(capacity);
            int minCount = Math.Min(capacity, _availableItems.Count);
            for (int i = 0; i < minCount; i++)
                availableItems.Add(_availableItems[i]);
            _availableItems = availableItems;

            _availableItemsSet.Clear();
            _availableItemsSet.UnionWith(_availableItems);

            _version++;
        }

        #endregion

        #region Pooling

        public void Register(TItem item)
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered(TItem item) => _registeredItems.ContainsKey(item);

        public bool IsAvailable(TItem item) => _availableItemsSet.ContainsKey(item);

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