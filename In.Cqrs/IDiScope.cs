namespace In.Cqrs
{
    public interface IDiScope
    {
        T Resolve<T>();
    }
}