using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    internal sealed partial class StrictPoolStorage<T> : IPoolStorage<T> where T : class
    {
        #region Fields

        private int _version;
        private List<T> _availableItems = new List<T>();
        private readonly HashSet<T> _usedItems = new HashSet<T>(); 

        #endregion

        #region Properties

        public int Count
        {
            get { return _availableItems.Count + _usedItems.Count; }
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

        #region Storage Management

        public void Register(T item)
        {
            if (_availableItems.Contains(item) || _usedItems.Contains(item))
                throw new ArgumentException("Cannot register the same object twice", nameof(item));

            _availableItems.Add(item);
            _version++;
        }

        public void Reset(bool destroyItems)
        {
            if (destroyItems)
                DestroyAll();
            else
                ReleaseAll();
        }

        public void SetCapacity(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            List<T> availableItems = new List<T>(capacity);
            availableItems.AddRange(_availableItems);
            _availableItems = availableItems;
            _version++;
        }

        #endregion

        #region Pooling

        public T Get()
        {
            if (_availableItems.Count == 0)
                return null;

            T item = _availableItems[_availableItems.Count - 1];
            _usedItems.Add(item);
            _availableItems.RemoveAt(_availableItems.Count - 1);
            _version++;

            return item;
        }

        public void Set(T item)
        {
            if (!_usedItems.Contains(item))
                throw new ArgumentException("Failed to set an item already released or not created by the pool", nameof(item));

            _availableItems.Add(item);
            _usedItems.Remove(item);
            _version++;
        }

        private void ReleaseAll()
        {
            _availableItems.AddRange(_usedItems);
            _usedItems.Clear();
            _version++;
        }

        private void DestroyAll()
        {
            _availableItems.Clear();
            _usedItems.Clear();
            _version++;
        }

        #endregion
    }
}