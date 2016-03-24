using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos
{
    internal sealed partial class FlexiblePoolStorage<T> : IPoolStorage<T> where T : class
    {
        #region Fields

        private List<T> _availableItems = new List<T>();
        private readonly HashSet<T> _availableItemsTable = new HashSet<T>();
        private int _version;

        #endregion

        #region Properties

        public int Count => _availableItems.Count;

        #endregion

        #region Storage Management

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            List<T> availableItems = new List<T>(capacity);
            int minCount = Math.Min(capacity, _availableItems.Count);
            for (int i = 0; i < minCount; i++)
                availableItems.Add(_availableItems[i]);
            _availableItems = availableItems;

            _availableItemsTable.Clear();
            _availableItemsTable.UnionWith(_availableItems);

            _version++;
        }

        public void Register(T item)
        {
            Set(item);
        }

        public void ResetItems()
        {
        }

        public void DestroyItems()
        {
            _availableItems.Clear();
            _availableItemsTable.Clear();
            _version++;
        }

        #endregion

        #region Pooling

        public T Get()
        {
            if (_availableItems.Count == 0)
                return null;

            T item = _availableItems[_availableItems.Count - 1];
            _availableItems.RemoveAt(_availableItems.Count - 1);
            _availableItemsTable.Remove(item);
            _version++;

            return item;
        }

        public void Set(T item)
        {
            if (_availableItemsTable.Contains(item))
                throw new ArgumentException("Failed to set an item already released", nameof(item));

            _availableItems.Add(item);
            _availableItemsTable.Add(item);
            _version++;
        }

        #endregion

        #region Enumerator

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion
    }
}
