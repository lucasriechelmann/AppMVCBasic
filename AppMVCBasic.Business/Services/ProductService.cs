using AppMVCBasic.Business.Interfaces;
using AppMVCBasic.Business.Models;
using AppMVCBasic.Business.Models.Validations;

namespace AppMVCBasic.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task AddAsync(Product product)
        {
            if (!IsEntityValid(new ProductValidation(), product))
                return;

            await _productRepository.AddAsync(product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }

        public async Task UpdateAsync(Product product)
        {
            if (!IsEntityValid(new ProductValidation(), product))
                return;

            await _productRepository.UpdateAsync(product);
        }
    }
}
