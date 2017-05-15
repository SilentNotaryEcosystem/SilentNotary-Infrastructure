﻿using In.Cqrs.Query.Criterion.Abstract;
using In.Cqrs.Query.Criterion.Abstract;

namespace In.Cqrs.Query
{
    /// <summary>
    ///     Фабрика объектов запросов
    /// </summary>
    public interface IQueryFactory
    {
        /// <summary>
        ///     Создать запрос, возвращающий необходимые результаты с определенными критериями
        /// </summary>
        /// <typeparam name="TCriterion"> </typeparam>
        /// <typeparam name="TResult"> </typeparam>
        /// <returns> </returns>
        IQuery<TCriterion, TResult> Create<TCriterion, TResult>()
            where TCriterion : ICriterion;
    }
}