using System;
using System.Collections.Generic;

namespace Atomos.Collections.Extension
{
    public static class ListExtension
    {
        #region IList Extensions

        public static int BinarySearch<TItem, TValue>(this IList<TItem> source, TValue value, Func<TItem, TValue, int> comparer)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if(comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            int low = 0;
            int high = source.Count - 1;
            while (low <= high)
            {
                int middle = low + ((high - low) >> 1);
                int comparison = comparer(source[middle], value);
                if (comparison == 0)
                    return middle;

                if (comparison > 0)
                    high = middle - 1;
                else
                    low = middle + 1;
            }

            return ~low;
        }

        #endregion
    }
}