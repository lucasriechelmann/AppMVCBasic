using AppMVCBasic.Business.Models;

namespace AppMVCBasic.Business.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressBySupplierAsync(Guid supplierId);
    }
}
