using System;
using System.Collections;

namespace Atomos.Tests.Pool
{
    public abstract partial class CollectionPool_Generic_Test_Any<TPool, TItem> : CollectionPool_Generic_Test<TPool, TItem>
        where TPool : CollectionPool<TItem>, IPool<TItem>
        where TItem : class, ICollection
    {
        #region Constructors

        protected CollectionPool_Generic_Test_Any(Func<CollectionPoolSettings<TItem>, TPool> factory) 
            : base(factory, CollectionPoolMode.Any)
        {
        }

        #endregion
    }
}