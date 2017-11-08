using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using In.Cqrs.Query;
using SmartDotNet.Cqrs.Domain.Interfaces;
using SmartDotNet.Cqrs.Specifications;

namespace SmartDotNet.Cqrs.Queries
{
    public class ProjectionQuery<TSource, TDest>
        : IQuery<Specification<TSource>, IEnumerable<TDest>>
        where TSource : class, IHasKey
        where TDest : class
    {
        private readonly ILinqProvider _linqProvider;
        private readonly MapperConfiguration _mapperConfiguration;

        public ProjectionQuery(ILinqProvider linqProvider, MapperConfiguration mapperConfigurationProvider)
        {
            _linqProvider = linqProvider;
            _mapperConfiguration = mapperConfigurationProvider;
        }

        protected virtual IQueryable<TDest> Query(Specification<TSource> spec)
            => _linqProvider.Query<TSource>()
                .Where(spec)
                .ProjectTo<TDest>(_mapperConfiguration);

        async Task<IEnumerable<TDest>> IQuery<Specification<TSource>, IEnumerable<TDest>>.Ask(
            Specification<TSource> spec)
            => await Query(spec)
                .ToArrayAsync();
    }
}