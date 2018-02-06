using In.Legacy.Query.Criterion.Abstract;

namespace In.Legacy.Query.Criterion
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
