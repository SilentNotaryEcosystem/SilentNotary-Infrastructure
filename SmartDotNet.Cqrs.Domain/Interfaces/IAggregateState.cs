namespace SmartDotNet.Cqrs.Domain.Interfaces
{
    public interface IAggregateState
    {
        IAggregateRoot Aggregate { get; set; }
    }
}
