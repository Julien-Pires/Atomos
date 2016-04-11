using System;
using System.Collections.Generic;

namespace Atomos.Collections.Extension
{
    public static class DictionaryExtension
    {
        #region Extensions

        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> target, IDictionary<TKey, TValue> source)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if(source == null)
                throw new ArgumentNullException(nameof(source));

            foreach (var pair in source)
                target[pair.Key] = pair.Value;
        }

        #endregion
    }
}