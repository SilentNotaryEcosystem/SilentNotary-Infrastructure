using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using SmartDotNet.Cqrs.Domain.Interfaces;
using SmartDotNet.Cqrs.Specifications;

namespace SmartDotNet.Cqrs.Queries.Impls
{
    public class GenericQueryBuilder<TSource> : GenericQuery<TSource>, IGenericQueryBuilder<TSource> where TSource : class, IHasKey
    {
        private readonly IConfigurationProvider _mapperConfiguration;

        public GenericQueryBuilder(ILinqProvider linqProvider, IConfigurationProvider mapperConfigurationProvider)
            : base(linqProvider.Query<TSource>())
        {
            _mapperConfiguration = mapperConfigurationProvider;
        }

        public IGenericQuery<TDest> ProjectTo<TDest>()
            where TDest : class
        {
            return new GenericQuery<TDest>(Queryable.ProjectTo<TDest>(_mapperConfiguration));
        }

        public IGenericQueryBuilder<TSource> Where(Specification<TSource> specification)
        {
            Queryable = Queryable.Where(specification);
            return this;
        }

        public IGenericQueryBuilder<TSource> OrderBy<TKey>(Expression<Func<TSource, TKey>> keySelector,
            bool descending = false)
        {
            Queryable = descending ? Queryable.OrderByDescending(keySelector) : Queryable.OrderBy(keySelector);
            return this;
        }

        public async Task<int> CountAsync()
        {
            return await Queryable.CountAsync();
        }

        public Task<TSource> MinAsync()
        {
            return Queryable.MinAsync();
        }

        public Task<TSource> MaxAsync()
        {
            return Queryable.MaxAsync();
        }
    }
}