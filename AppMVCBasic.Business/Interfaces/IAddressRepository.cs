using AppMVCBasic.Business.Models;

namespace AppMVCBasic.Business.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressBySupplierAsync(Guid supplierId);
    }
}
