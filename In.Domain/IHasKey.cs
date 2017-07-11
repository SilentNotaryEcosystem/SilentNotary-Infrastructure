namespace In.Domain
{
    public interface IHasKey
    {
        object GetId();
    }

    public interface IHasKey<TId> : IHasKey
    {
        TId Id { get; set; }
    }
}
