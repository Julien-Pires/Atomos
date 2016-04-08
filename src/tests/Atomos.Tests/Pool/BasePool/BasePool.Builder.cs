using System;

namespace Atomos.Tests.Pool
{
    public class BasePool_Builder<TPool, TItem, TSettings> : IPool_Builder<TPool, TItem>
        where TPool : IPool<TItem>
        where TItem : class
        where TSettings : PoolSettings<TItem>
    {
        #region Fields

        private readonly Func<TSettings, TPool> _factory; 
        private int _initialCapacity;
        private PoolingMode _poolMode;
        private Action<TItem> _reset;
        private Func<TItem> _initializer;

        #endregion

        #region Constructors

        public BasePool_Builder(Func<TSettings, TPool> factory)
        {
            _factory = factory;
        }

        #endregion

        #region Methods

        public TPool Build()
        {
            return _factory(PrepareSettings());
        }

        protected virtual TSettings CreateSettings()
        {
            return new PoolSettings<TItem>() as TSettings;
        }

        protected virtual TSettings PrepareSettings()
        {
            TSettings settings = CreateSettings();
            settings.Mode = _poolMode;
            settings.Capacity = _initialCapacity;
            settings.Initializer = _initializer;
            settings.Reset = _reset;

            return settings;
        }

        public static implicit operator TPool(BasePool_Builder<TPool, TItem, TSettings> builder)
        {
            return builder.Build();
        }

        public BasePool_Builder<TPool, TItem, TSettings> WithInitialCapacity(int initialCapacity)
        {
            _initialCapacity = initialCapacity;

            return this;
        }

        public BasePool_Builder<TPool, TItem, TSettings> WithPoolingMode(PoolingMode mode)
        {
            _poolMode = mode;

            return this;
        }

        public BasePool_Builder<TPool, TItem, TSettings> WithInitializer(Func<TItem> intializer)
        {
            _initializer = intializer;

            return this;
        }

        public BasePool_Builder<TPool, TItem, TSettings> WithReset(Action<TItem> reset)
        {
            _reset = reset;

            return this;
        }

        #endregion
    }
}