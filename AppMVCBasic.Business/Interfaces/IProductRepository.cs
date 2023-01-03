using AppMVCBasic.Business.Models;

namespace AppMVCBasic.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductsBySupplierAsync(Guid supplierId);
        Task<List<Product>> GetProductsSuppliersAsync();
        Task<Product> GetProductSupplierAsync(Guid id);
    }
}
