using System.Threading.Tasks;

namespace In.Legacy.Entity.Uow
{
    public interface IAsyncCrudUow<TEntity, in TKey>
    {
        Task<TEntity> GetById(TKey id);
        Task<TEntity[]> Get();
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TKey id);
    }
}
