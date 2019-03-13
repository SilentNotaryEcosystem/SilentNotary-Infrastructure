using SilentNotary.Cqrs.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SilentNotary.Common;

namespace SilentNotary.Cqrs.Domain
{
    public abstract class DomainEntityBase
    {
        public void MergeValueData(object other, IValueObjectProvider provider)
        {
            var thisType = GetType();
            var otherType = other.GetType();

            if (thisType != otherType)
                throw new InvalidOperationException("entities should be exactly of the same type");

            var properties = thisType.GetProperties()
                .Where(p => Attribute.IsDefined(p, typeof(ValueDataAttribute)));

            foreach (var property in properties)
            {
                var valOther = property.GetMethod.Invoke(other, null);
                var propTypeInfo = property.PropertyType.GetTypeInfo();
                var setMethod = property.GetSetMethod(true);

                if (propTypeInfo.IsConstructedGenericType &&
                    propTypeInfo.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    var entryType = propTypeInfo.GenericTypeArguments[0];
                    if (typeof(IPersistableValueObject).IsAssignableFrom(entryType))
                    {
                        var srcCollection = valOther;
                        var destCollection = provider.GetType()
                            .GetMethod(nameof(provider.PopulateCollection))
                            .MakeGenericMethod(entryType)
                            .Invoke(provider, new[] { srcCollection });

                        setMethod.Invoke(this, new[] { destCollection });
                        continue;
                    }
                }

                if (typeof(IPersistableValueObject).IsAssignableFrom(propTypeInfo) && valOther != null)
                {
                    valOther = provider.GetType()
                        .GetMethod(nameof(provider.PopulateObject))
                        .MakeGenericMethod(propTypeInfo)
                        .Invoke(provider, new[] { valOther });                    
                }

                setMethod.Invoke(this, new[] { valOther });
            }
        }
    }

    public abstract class DomainEntityBase<TId> : DomainEntityBase, IHasKey<TId>, IEquatable<DomainEntityBase<TId>>
    {
        public virtual TId Id { get; set; }

        public bool IsTransient()
        {
            return Id.Equals(default(TId));
        }

        public override bool Equals(object obj)
        {
            if (obj is DomainEntityBase<TId> other)
                return Equals(other);
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(DomainEntityBase<TId> a, DomainEntityBase<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.Equals(b);
        }

        public static bool operator !=(DomainEntityBase<TId> a, DomainEntityBase<TId> b)
        {
            return !(a == b);
        }

        #region IEquatable implementation
        public bool Equals(DomainEntityBase<TId> other)
        {
            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(other, this))
                return true;

            if (other.Id.Equals(Id) && !IsTransient() && !other.IsTransient())
                return true;

            return false;
        }
        #endregion  
    }
}
