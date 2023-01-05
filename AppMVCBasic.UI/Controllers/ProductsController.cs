using AppMVCBasic.Business.Interfaces;
using AppMVCBasic.Business.Models;
using AppMVCBasic.UI.Extensions;
using AppMVCBasic.UI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppMVCBasic.UI.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        readonly IProductRepository _productRepository;
        readonly ISupplierRepository _supplierRepository;
        readonly IProductService _productService;
        readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, 
            ISupplierRepository supplierRepository,
            IProductService productService, 
            IMapper mapper,
            INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _productService = productService;
            _mapper = mapper;
        }

        // GET: Products
        [Route("list-of-products")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {            
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsSuppliersAsync()));
        }

        // GET: Products/Details/5
        [Route("data-of-product/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProductAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }
        [Route("new-product")]
        // GET: Products/Create
        [ClaimsAuthorize("Product", "Add")]
        public async Task<IActionResult> Create()
        {
            var productViewModel = await SetSuppliersAsync(new ProductViewModel());
            return View(productViewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new-product")]
        [ClaimsAuthorize("Product", "Add")]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                productViewModel = await SetSuppliersAsync(productViewModel);
                return View(productViewModel);
            }

            var imgPrefix = $"{Guid.NewGuid()}_";

            if (!await UploadImage(productViewModel.ImageUpload, imgPrefix))
            {
                productViewModel = await SetSuppliersAsync(productViewModel);
                return View(productViewModel);
            }

            productViewModel.Image = $"{imgPrefix}{productViewModel.ImageUpload.FileName}";

            await _productService.AddAsync(_mapper.Map<Product>(productViewModel));

            if (!IsRequestValid())
            {
                productViewModel = await SetSuppliersAsync(productViewModel);
                return View(productViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        [Route("edit-product/{id:guid}")]
        [ClaimsAuthorize("Product", "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProductAsync(id);
            
            if (productViewModel == null)
            {
                return NotFound();
            }
            
            return View(productViewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit-product/{id:guid}")]
        [ClaimsAuthorize("Product", "Edit")]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
                return NotFound();

            var product = await GetProductAsync(id);
            productViewModel.Supplier = product.Supplier;
            productViewModel.Image = product.Image;

            if (!ModelState.IsValid)
                return View(productViewModel);

            if(productViewModel.ImageUpload is not null)
            {
                var imgPrefix = $"{Guid.NewGuid()}_";

                if (!await UploadImage(productViewModel.ImageUpload, imgPrefix))
                {
                    productViewModel = await SetSuppliersAsync(productViewModel);
                    return View(productViewModel);
                }

                product.Image = $"{imgPrefix}{productViewModel.ImageUpload.FileName}";
            }

            product.Name = productViewModel.Name;
            product.Description= productViewModel.Description;
            product.Value= productViewModel.Value;
            product.Active= productViewModel.Active;

            await _productService.UpdateAsync(_mapper.Map<Product>(product));

            if (!IsRequestValid())
            {
                productViewModel = await SetSuppliersAsync(productViewModel);
                return View(productViewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        [Route("delete-product/{id:guid}")]
        [ClaimsAuthorize("Product", "Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var productViewModel = await GetProductAsync(id);
            if (productViewModel == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete-product/{id:guid}")]
        [ClaimsAuthorize("Product", "Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productViewModel = await GetProductAsync(id);

            if (productViewModel == null)
            {
                return NotFound();
            }

            await _productService.DeleteAsync(id);

            if (!IsRequestValid())
                return View(productViewModel);

            TempData["Success"] = "The product has been deleted.";

            return RedirectToAction(nameof(Index));
        }

        async Task<ProductViewModel> GetProductAsync(Guid Id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductSupplierAsync(Id));            
            return await SetSuppliersAsync(product);
        }
        async Task<ProductViewModel> SetSuppliersAsync(ProductViewModel product)
        {
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAsync());
            return product;
        }

        async Task<bool> UploadImage(IFormFile file, string imgPrefix)
        {
            if(file.Length <=0 ) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{imgPrefix}{file.FileName}");

            if(System.IO.File.Exists(path) )
            {
                ModelState.AddModelError(string.Empty, "There is a file with this name.");
                return false;
            }

            using(var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);                
            }

            return true;
        }
    }
}
