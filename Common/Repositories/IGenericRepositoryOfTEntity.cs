using System.Linq;
using System.Threading.Tasks;


namespace Common.Repositories
{
    public interface IGenericRepository<TEntity>
        : IRepository where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAll();

        Task<TEntity> GetById(long entityKey);

        Task<long> Add(TEntity entity); 

        Task Update(TEntity entity);

        Task Remove(TEntity entity);

        Task Remove(long entityKey);
    }
}
