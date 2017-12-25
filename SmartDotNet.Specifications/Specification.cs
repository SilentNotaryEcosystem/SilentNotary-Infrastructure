using System;
using System.Linq.Expressions;
using SmartDotNet.Specifications.Helpers.BooleanOperators;

namespace SmartDotNet.Specifications
{
    public abstract class Specification<T>
    {
        internal abstract Expression<Func<T, bool>> ToExpression();

        public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
        {
            return spec.ToExpression();
        }

        /// <summary>
        /// Override operator true for supporting short-circuit &amp &amp and || operators
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static bool operator true(Specification<T> spec)
        {
            return false;
        }

        /// <summary>
        /// Override operator false for supporting short-circuit &amp &amp and || operators
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public static bool operator false(Specification<T> spec)
        {
            return false;
        }

        public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2)
        {
            return new AndSpecification<T>(spec1, spec2);
        }

        public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2)
        {
            return new OrSpecification<T>(spec1, spec2);
        }

        public static Specification<T> operator !(Specification<T> spec)
        {
            return new NotSpecification<T>(spec);
        }
    }
}
