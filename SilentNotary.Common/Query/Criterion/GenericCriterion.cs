using SilentNotary.Common.Query.Criterion.Abstract;

namespace SilentNotary.Common.Query.Criterion
{
    public class GenericCriterion<T> : IGenericCriterion<T>
    {
        public T Value { get; set; }
        public GenericCriterion(T value)
        {
            Value = value;
        }
    }
}
