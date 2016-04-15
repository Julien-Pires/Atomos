using System.Collections.Generic;

namespace Atomos
{
    public sealed partial class ListPool<TItem> : CollectionPool<List<TItem>>
    {
        #region Fields

        private static readonly ListPoolHelper Helper = new ListPoolHelper();

        #endregion

        #region Constructors

        public ListPool(CollectionPoolSettings<List<TItem>> settings)
            : base(settings,
                  new CollectionPoolItemFactory<List<TItem>>(c => new List<TItem>(c), settings),
                  Helper)
        {
        }

        #endregion
    }
}