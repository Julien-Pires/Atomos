using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos
{
    internal class PoolStorage<T> : IPoolStorage<T> where T : class
    {
        #region Fields

        private int _version;
        private List<T> _availableItems = new List<T>();
        private readonly HashSet<T> _availableItemsSet = new HashSet<T>();
        private readonly HashSet<T> _registeredItems = new HashSet<T>();
        
        #endregion

        public int Count => _availableItems.Count;

        public void DestroyItems()
        {
            throw new NotImplementedException();
        }

        public T Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered(T item)
        {
            throw new NotImplementedException();
        }

        public void Register(T item)
        {
            _availableItems.Add(item);
            _availableItemsSet.Add(item);
            _registeredItems.Add(item);

            _version++;
        }

        public void ResetItems()
        {
            throw new NotImplementedException();
        }

        public void Set(T item)
        {
            throw new NotImplementedException();
        }

        public void SetCapacity(int capacity)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}