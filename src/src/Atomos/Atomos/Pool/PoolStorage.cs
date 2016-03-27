using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos
{
    internal sealed partial class PoolStorage<T> : IPoolStorage<T> where T : class
    {
        #region Fields

        private int _version;
        private List<T> _availableItems = new List<T>();
        private readonly HashSet<T> _availableItemsSet = new HashSet<T>();
        private readonly HashSet<T> _registeredItems = new HashSet<T>();

        #endregion

        #region Properties

        public int Count => _availableItems.Count;

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
            _availableItems.Clear();
            _availableItems.AddRange(_availableItemsSet);

            _version++;
        }

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            List<T> availableItems = new List<T>(capacity);
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

        public void Register(T item)
        {
            _registeredItems.Add(item);
            _version++;
        }

        public bool IsRegistered(T item)
        {
            return _registeredItems.Contains(item);
        }

        public bool IsAvailable(T item)
        {
            return _availableItemsSet.Contains(item);
        }

        public void Set<TParam>(T item, TParam parameter)
        {
            _availableItems.Add(item);
            _availableItemsSet.Add(item);

            _version++;
        }

        public T Get<TParam>(TParam parameter)
        {
            if (_availableItems.Count == 0)
                return null;

            int index = _availableItems.Count - 1;
            T item = _availableItems[index];
            _availableItems.RemoveAt(index);
            _availableItemsSet.Remove(item);

            _version++;

            return item;
        }

        #endregion
    }
}