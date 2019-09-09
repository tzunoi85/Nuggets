using System.Linq;
using System.Threading.Tasks;


namespace Common.OData
{
   public interface IQueryOptions
    {
        Task<IQueryable> ApplyQuery(IQueryable collection);
    }
}
