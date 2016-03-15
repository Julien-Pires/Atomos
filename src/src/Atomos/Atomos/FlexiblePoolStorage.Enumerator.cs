using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    internal sealed partial class FlexiblePoolStorage<T> : IPoolStorage<T> where T : class
    {
        #region Nested

        public struct Enumerator : IEnumerator<T>
        {
            #region Fields

            private readonly FlexiblePoolStorage<T> _storage;
            private int _index;
            private int _version;
            private T _current;

            #endregion

            #region Properties

            public T Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return _current; }
            }

            #endregion

            #region Constructors

            internal Enumerator(FlexiblePoolStorage<T> storage)
            {
                _storage = storage;
                _index = 0;
                _version = storage._version;
                _current = default(T);
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
                bool canMove = (_index < _storage._availableItems.Count) && (_storage._version == _version);
                if (canMove)
                {
                    _current = _storage._availableItems[_index];
                    _index++;
                }
                else
                {
                    _current = default(T);
                    _index = _storage._availableItems.Count + 1;
                }

                return canMove;
            }

            void IEnumerator.Reset()
            {
                if (_storage._version != _version)
                    throw new InvalidOperationException("Failed to reset iterator because the storage has been modified");

                _index = 0;
                _current = default(T);
            }

            #endregion
        }

        #endregion
    }
}
