namespace In.Legacy.Query.Criterion.Abstract
{
    /// <summary>
    ///     Критерии запроса
    /// </summary>
    public interface IGenericCriterion<T> : ICriterion
    {
        T Value { get; set; }
    }
}