using AppMVCBasic.Business.Models;

namespace AppMVCBasic.Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetSupplierAddressAsync(Guid id);
        Task<Supplier> GetSupplierProductsAddressAsync(Guid id);
    }
}
