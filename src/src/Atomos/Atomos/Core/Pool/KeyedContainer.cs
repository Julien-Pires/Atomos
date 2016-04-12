using System.Collections.Generic;

namespace Atomos
{
    internal partial class KeyedPoolStorage<TItem, TKey>
    {
        internal class KeyedContainer
        {
            #region Fields

            private readonly List<TItem> _items = new List<TItem>(); 

            #endregion

            #region Properties

            public TKey Key { get; }

            public bool HasValue => _items.Count > 0;

            #endregion

            #region Constructors

            public KeyedContainer(TKey key)
            {
                Key = key;
            }

            #endregion

            #region Methods

            public TItem Get()
            {
                if (!HasValue)
                    return null;

                int index = _items.Count - 1;
                TItem item = _items[index];
                _items.RemoveAt(index);

                return item;
            }

            public void Set(TItem item)
            {
                _items.Add(item);
            }

            public void Clear()
            {
                _items.Clear();
            }

            #endregion
        }
    }
}