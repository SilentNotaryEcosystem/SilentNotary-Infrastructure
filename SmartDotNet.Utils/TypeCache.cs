using System.Collections.Concurrent;
using System.Threading;

namespace SmartDotNet.Utils
{
    public class TypeCache<TPar, TRes> : ITypeCache<TPar, TRes>
    {
        private readonly ConcurrentDictionary<TPar, TRes> _dictionary;
        private readonly ReaderWriterLockSlim _lockSlim;

        public TypeCache()
        {
            _dictionary = new ConcurrentDictionary<TPar, TRes>();
            _lockSlim = new ReaderWriterLockSlim();
        }

        public TRes Get(TPar par)
        {
            _lockSlim.EnterReadLock();
            try
            {
                TRes res;
                if (!_dictionary.TryGetValue(par, out res))
                {
                    return default(TRes);
                }

                return res;
            }
            finally
            {
                _lockSlim.ExitReadLock();
            }
        }

        public void Set(TPar par, TRes res)
        {
            _lockSlim.EnterWriteLock();
            try
            {
                _dictionary[par] = res;
            }
            finally
            {
                _lockSlim.ExitWriteLock();
            }
        }
    }
}
