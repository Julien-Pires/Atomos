using Atomos;

namespace Atomos.Tests.Pool
{
    public abstract partial class SharedPool_Generic_Test<T> where T : class 
    {
        public SharedPool_Generic_Test()
        {
            SharedPool<T>.DestroyAll();
        }
    }
}
