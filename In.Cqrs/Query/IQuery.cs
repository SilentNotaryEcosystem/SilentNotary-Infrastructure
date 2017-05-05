﻿namespace In.Cqrs.Query
{
    /// <summary>
    ///     Интерфейс для объектов запросов к базе
    /// </summary>
    /// <typeparam name="TCriterion"> </typeparam>
    /// <typeparam name="TResult"> </typeparam>
    public interface IQuery<in TCriterion, out TResult>
        where TCriterion : ICriterion
    {
        /// <summary>
        ///     Получить результат из базы
        /// </summary>
        /// <param name="criterion"> </param>
        /// <returns> </returns>
        TResult Ask(TCriterion criterion);
    }
}