using System.Threading.Tasks;

namespace Common.Repositories
{
    public interface IUnitOfWork
    {
        TRepository GetRepository<TRepository>() where TRepository : IRepository;

        Task SaveChanges();
    }
}
