using System;

namespace Atomos
{
    internal sealed class DefaultPoolItemFactory<TItem, TParam> : IPoolItemFactory<TItem, TParam>
        where TItem : class
    {
        #region Fields

        private readonly Func<TItem> _factory; 

        #endregion

        #region Constructors

        public DefaultPoolItemFactory(Func<TItem> factory)
        {
            if(factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        } 

        #endregion

        #region Factory

        public TItem Create(TParam parameter)
        {
            return _factory();
        }

        #endregion
    }
}