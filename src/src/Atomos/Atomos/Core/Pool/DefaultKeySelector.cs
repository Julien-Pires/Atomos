using System;

namespace Atomos
{
    public sealed class DefaultKeySelector<TItem, TKey> : IKeySelector<TItem, TKey>
        where TItem : class
    {
        #region Fields

        private readonly Func<TItem, TKey> _selectorFunc; 

        #endregion

        #region Constructor

        public DefaultKeySelector(Func<TItem, TKey> selectorFunc)
        {
            _selectorFunc = selectorFunc;
        }

        #endregion

        #region Selector

        public TKey GetKey(TItem item)
        {
            return _selectorFunc(item);
        }

        #endregion
    }
}