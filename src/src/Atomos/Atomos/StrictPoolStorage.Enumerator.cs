using System;
using System.Collections;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    internal sealed partial class StrictPoolStorage<T>
    {
        #region Nested

        public struct Enumerator : IEnumerator<T>
        {
            #region Fields

            private readonly StrictPoolStorage<T> _storage;
            private int _index;
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

			internal Enumerator(StrictPoolStorage<T> storage)
            {
                _storage = storage;
                _index = 0;
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
                bool canMove = _index < _storage._availableItems.Count;
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

            public void Reset()
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion
    }
}