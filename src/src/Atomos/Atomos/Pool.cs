﻿using System;
using System.Collections.Generic;

namespace Atomos.Atomos
{
    public class Pool<T> : IDisposable where T : class
    {
        #region Properties

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Constructors

        public Pool(PoolSettings<T>? settings = null)
        {
        }

        #endregion

        #region Cleanup

        public void Reset(bool cleanItems = false)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Pooling

        public T Get()
        {
            throw new NotImplementedException();
        }

        public void Set(T item)
        {
            throw new NotImplementedException();
        }

        public void Set(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}