namespace In.Legacy
{
    public interface IDiScope
    {
        T Resolve<T>();
    }
}