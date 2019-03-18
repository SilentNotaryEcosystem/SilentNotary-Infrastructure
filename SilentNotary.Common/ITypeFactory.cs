using System;

namespace SilentNotary.Common
{
    public interface ITypeFactory
    {
        Type Get(string name);
    }
}