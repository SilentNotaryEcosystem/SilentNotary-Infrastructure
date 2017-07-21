namespace SmartDotNet.Cqrs.Domain
{
    public abstract class Aggregate
    {
        
    }

    public abstract class Aggregate<TRoot> : Aggregate
        where TRoot: IAggregateRoot
    {
        public TRoot Root { get; }

        public Aggregate(TRoot root)
        {
            Root = root;
        }
    }
}
