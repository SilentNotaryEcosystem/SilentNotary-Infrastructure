using System;
using System.Linq.Expressions;

namespace SilentNotary.Common.Query.Criterion.Abstract
{
    public interface IExpressionCriterion<T> : IGenericCriterion<T>
    {
        Expression<Func<T, bool>> Get();
    }
}
