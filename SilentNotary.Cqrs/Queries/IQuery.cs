using System.Threading.Tasks;
using SilentNotary.Common.Query.Criterion.Abstract;

namespace SilentNotary.Cqrs.Queries
{
    /// <summary>
    ///     Интерфейс для объектов запросов к базе
    /// </summary>
    /// <typeparam name="TCriterion"> </typeparam>
    /// <typeparam name="TResult"> </typeparam>
    public interface IQuery<in TCriterion, TResult> where TCriterion : ICriterion
    {
        /// <summary>
        ///     Получить результат из базы
        /// </summary>
        /// <param name="criterion"> </param>
        /// <returns> </returns>
        Task<TResult> Ask(TCriterion criterion);
    }
}