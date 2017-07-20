namespace SmartDotNet.Cqrs.Domain
{
    public interface IHasKey
    {
    }

    public interface IHasKey<out TId> : IHasKey
    {
        TId Id { get; }
    }
}
