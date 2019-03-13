namespace SilentNotary.Common
{
    public interface IHasKey
    {
    }

    public interface IHasKey<out TId> : IHasKey
    {
        TId Id { get; }
    }
}
