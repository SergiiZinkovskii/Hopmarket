using Market.Domain.Entity;

namespace Market.DAL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product?> Find(long id, CancellationToken cancellationToken);
    }
}