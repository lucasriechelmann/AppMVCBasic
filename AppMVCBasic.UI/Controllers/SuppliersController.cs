using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMVCBasic.UI.Data;
using AppMVCBasic.UI.Models;
using AppMVCBasic.Business.Interfaces;
using AutoMapper;
using AppMVCBasic.Business.Models;
using Microsoft.AspNetCore.Authorization;
using AppMVCBasic.UI.Extensions;

namespace AppMVCBasic.UI.Controllers
{
    [Authorize]
    public class SuppliersController : BaseController
    {
        readonly ISupplierRepository _supplierRepository;
        readonly ISupplierService _supplierService;
        readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository, 
            ISupplierService supplierService, 
            IMapper mapper,
            INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        // GET: Suppliers
        [Route("list-of-suppliers")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAsync()));
        }

        // GET: Suppliers/Details/5
        [Route("data-of-supplier/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetSupplierAddressAsync(id);

            if (supplierViewModel is null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        // GET: Suppliers/Create
        [Route("new-supplier")]
        [ClaimsAuthorize("Supplier", "Add")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("new-supplier")]
        [ClaimsAuthorize("Supplier", "Add")]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
                return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierService.AddAsync(supplier);

            if (!IsRequestValid())
                return View(supplierViewModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Edit/5
        [Route("edit-supplier/{id:guid}")]
        [ClaimsAuthorize("Supplier", "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            
            var supplierViewModel = await GetSupplierProductsAddressAsync(id);
            if (supplierViewModel == null)
            {
                return NotFound();
            }
            return View(supplierViewModel);
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit-supplier/{id:guid}")]
        [ClaimsAuthorize("Supplier", "Edit")]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierService.UpdateAsync(supplier);

            if (!IsRequestValid())
                return View(supplierViewModel);

            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Delete/5
        [Route("delete-supplier/{id:guid}")]
        [ClaimsAuthorize("Supplier", "Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = await GetSupplierAddressAsync(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("delete-supplier/{id:guid}")]
        [ClaimsAuthorize("Supplier", "Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = await GetSupplierAddressAsync(id);

            if(supplierViewModel is null)
                return NotFound();

            await _supplierService.DeleteAsync(id);

            if (!IsRequestValid())
                return View(supplierViewModel);

            return RedirectToAction(nameof(Index));
        }
        [Route("get-supplier-address/{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = await GetSupplierAddressAsync(id);

            if(supplier is null)
                return NotFound();

            return PartialView("_AddressDetails", supplier);
        }
        //[Route("update-address-supplier/{id:guid}")]
        [Route("update-supplier-address/{id:guid}")]
        [ClaimsAuthorize("Supplier", "Edit")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplierViewModel = await GetSupplierAddressAsync(id);

            if(supplierViewModel is null)
                return NotFound();

            return PartialView("_AddressUpdate", new SupplierViewModel { Address = supplierViewModel.Address });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("update-supplier-address/{id:guid}")]
        [ClaimsAuthorize("Supplier", "Edit")]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplier)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document");

            if(!ModelState.IsValid) return PartialView("_AddressUpdate", new SupplierViewModel { Address = supplier.Address });

            await _supplierService.UpdateAddressAsync(_mapper.Map<Address>(supplier.Address));

            if (!IsRequestValid())
                return PartialView("_AddressUpdate", new SupplierViewModel { Address = supplier.Address });

            var url = Url.Action("GetAddress", "Suppliers", new { id = supplier.Address.SupplierId });
            return Json(new { success = true, url });
        }
        async Task<SupplierViewModel> GetSupplierAddressAsync(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddressAsync(id));
        }
        async Task<SupplierViewModel> GetSupplierProductsAddressAsync(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierProductsAddressAsync(id));
        }
    }
}
