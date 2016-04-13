using System;

namespace Atomos.Tests.Pool
{
    public class BasePool_Builder<TPool, TItem> : IPool_Builder<TPool, TItem>
        where TPool : IPool<TItem>
        where TItem : class
    {
        #region Fields

        private readonly Func<PoolSettings<TItem>, TPool> _factory; 
        private int _initialCapacity;
        private PoolingMode _poolMode;
        private Action<TItem> _reset;
        private Func<TItem> _initializer;

        #endregion

        #region Constructors

        public BasePool_Builder(Func<PoolSettings<TItem>, TPool> factory)
        {
            _factory = factory;
        }

        #endregion

        #region Methods

        public TPool Build()
        {
            return _factory((PoolSettings<TItem>)PrepareSettings());
        }

        protected virtual object CreateSettings()
        {
            return new PoolSettings<TItem>();
        }

        protected virtual object PrepareSettings()
        {
            PoolSettings<TItem> settings =(PoolSettings<TItem>) CreateSettings();
            settings.Mode = _poolMode;
            settings.Capacity = _initialCapacity;
            settings.Initializer = _initializer;
            settings.Reset = _reset;

            return settings;
        }

        public static implicit operator TPool(BasePool_Builder<TPool, TItem> builder)
        {
            return builder.Build();
        }

        public BasePool_Builder<TPool, TItem> WithInitialCapacity(int initialCapacity)
        {
            _initialCapacity = initialCapacity;

            return this;
        }

        public BasePool_Builder<TPool, TItem> WithPoolingMode(PoolingMode mode)
        {
            _poolMode = mode;

            return this;
        }

        public BasePool_Builder<TPool, TItem> WithInitializer(Func<TItem> intializer)
        {
            _initializer = intializer;

            return this;
        }

        public BasePool_Builder<TPool, TItem> WithReset(Action<TItem> reset)
        {
            _reset = reset;

            return this;
        }

        #endregion
    }
}