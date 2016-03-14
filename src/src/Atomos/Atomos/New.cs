using System;
using System.Linq.Expressions;

namespace Atomos.Atomos
{
    internal static class New<T>
    {
        #region Fields

        public static readonly Func<T> Create = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();

        #endregion
    }
}
