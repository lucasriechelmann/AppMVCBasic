using AppMVCBasic.Business.Models;

namespace AppMVCBasic.Business.Interfaces
{
    public interface IProductService : IDisposable
    {
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
