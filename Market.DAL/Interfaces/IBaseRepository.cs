using Market.Domain.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Market.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> Find(long id, CancellationToken cancellationToken);
        Task Create(T entity);

        IQueryable<T> GetAll();

        Task Delete(T entity);

        Task<T> Update(T entity);

    }
}