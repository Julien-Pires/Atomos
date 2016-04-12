using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Atomos.Collections.Extension;

namespace Atomos
{
    internal partial class KeyedPoolStorage<TItem, TKey> : IPoolStorage<TItem>
        where TItem : class
    {
        #region Fields

        private int _version;
        private readonly IKeySelector<TItem, TKey> _keySelector; 
        private readonly List<KeyedContainer> _availableItems = new List<KeyedContainer>();
        private readonly HashSet<TItem> _availableItemsSet = new HashSet<TItem>();
        private readonly HashSet<TItem> _registeredItems = new HashSet<TItem>();

        #endregion

        #region Constructors

        public KeyedPoolStorage(IKeySelector<TItem, TKey> keySelector)
        {
            _keySelector = keySelector;
        } 

        #endregion

        #region Properties

        public KeyedContainer this[int index] => _availableItems[index];

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
            _availableItemsSet.UnionWith(_registeredItems);
            foreach (KeyedContainer item in _availableItems)
                item.Clear();

            foreach (TItem item in _availableItemsSet)
                AddAvailableItems(item);

            _availableItems.RemoveAll(c => !c.HasValue);

            _version++;
        }

        private void AddAvailableItems(TItem item)
        {
            if(!_availableItemsSet.Add(item))
                return;

            KeyedContainer container = EnsureKeyContainer(_keySelector.GetKey(item));
            container.Set(item);
        }

        private KeyedContainer EnsureKeyContainer(TKey key)
        {
            int index = _availableItems.BinarySearch(key, (item, value) => Comparer<TKey>.Default.Compare(item.Key, value));
            if (index > -1)
                return _availableItems[index];

            KeyedContainer container = new KeyedContainer(key);
            _availableItems.Insert(~index, container);

            return container;
        }

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _availableItems.Clear();
            _availableItemsSet.Clear();

            var availableItems = _availableItemsSet.Take(Math.Min(capacity, _availableItemsSet.Count)).ToArray();
            foreach (TItem item in availableItems)
                AddAvailableItems(item);

            _version++;
        }

        #endregion

        #region Pooling

        public void Register(TItem item)
        {
            _registeredItems.Add(item);
            _version++;
        }

        public bool IsRegistered(TItem item) => _registeredItems.Contains(item);

        public bool IsAvailable(TItem item) => _availableItemsSet.Contains(item);

        public TItem Get(TKey key)
        {
            int index = _availableItems.BinarySearch(key, (item, value) => Comparer<TKey>.Default.Compare(item.Key, value));

            return index > -1 ? _availableItems[index].Get() : null;
        }

        TItem IPoolStorage<TItem>.Get()
        {
            throw new NotSupportedException("Keyed storage can't retrieve item without key");
        }

        public void Set(TItem item)
        {
            AddAvailableItems(item);
        }

        #endregion
    }
}