using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos
{
    public partial class KeyedPoolStorage<TItem, TKey>
    {
        public struct Enumerator : IEnumerator<TItem>
        {
            #region Fields

            private readonly KeyedPoolStorage<TItem, TKey> _storage;
            private readonly int _version;
            private int _keyIndex;
            private int _itemIndex;

            #endregion

            #region Properties

            public TItem Current { get; private set; }

            object IEnumerator.Current => Current;

            #endregion

            #region Constructors

            internal Enumerator(KeyedPoolStorage<TItem, TKey> storage)
            {
                _storage = storage;
                _keyIndex = 0;
                _itemIndex = 0;
                _version = storage._version;
                Current = default(TItem);
            }

            #endregion

            #region Clean

            public void Dispose()
            {
            }

            #endregion

            #region Enumerator

            public bool MoveNext()
            {
                while((_keyIndex < _storage.Count) && (_itemIndex >= _storage._availableItems[_keyIndex].Count))
                {
                    _keyIndex++;
                    _itemIndex = 0;
                }

                bool canMove = (_keyIndex < _storage.Count) && 
                               (_itemIndex < _storage._availableItems[_keyIndex].Count) &&
                               (_storage._version == _version);
                if (canMove)
                {
                    Current = _storage._availableItems[_keyIndex][_itemIndex];
                    _itemIndex++;
                }
                else
                {
                    Current = default(TItem);
                    _keyIndex = _storage._availableItems.Count + 1;
                    _itemIndex = -1;
                }

                return canMove;
            }

            void IEnumerator.Reset()
            {
                if (_storage._version != _version)
                    throw new InvalidOperationException("Failed to reset iterator because the storage has been modified");

                _keyIndex = 0;
                _itemIndex = 0;
                Current = default(TItem);
            }

            #endregion
        }
    }
}