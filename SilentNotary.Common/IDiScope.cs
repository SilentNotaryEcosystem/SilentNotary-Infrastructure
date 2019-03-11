using System;

namespace SilentNotary.Common
{
    public interface IDiScope
    {
        T Resolve<T>();
        object Resolve(Type type);
    }
}