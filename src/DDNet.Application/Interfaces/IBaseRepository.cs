using System.Threading.Tasks;

namespace DDNet.Application.Interfaces
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}
