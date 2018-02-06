using System;
using System.Linq.Expressions;

namespace In.Legacy.Query.Criterion.Abstract
{
    public interface IExpressionCriterion<T> : IGenericCriterion<T>
    {
        Expression<Func<T, bool>> Get();
    }
}
