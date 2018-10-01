namespace SilentNotary.Common
{
    public interface IDiScope
    {
        T Resolve<T>();
    }
}