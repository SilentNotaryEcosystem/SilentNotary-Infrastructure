using System.Collections.Generic;

namespace SmartDotNet.Cqrs.Domain
{
    public interface IValueObjectProvider
    {
        T PopulateObject<T>(T entity)
            where T : class, IPersistableValueObject;

        ICollection<T> PopulateCollection<T>(ICollection<T> source)
            where T : class, IPersistableValueObject;
    }
}
