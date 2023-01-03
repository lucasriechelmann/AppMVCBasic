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

namespace AppMVCBasic.UI.Controllers
{
    public class SuppliersController : BaseController
    {
        readonly ISupplierRepository _supplierRepository;
        readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
              return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAsync()));
        }

        // GET: Suppliers/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid)
                return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierRepository.AddAsync(supplier);

            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Edit/5
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
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierRepository.UpdateAsync(supplier);

            return RedirectToAction(nameof(Index));
        }

        // GET: Suppliers/Delete/5
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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplierViewModel = await GetSupplierAddressAsync(id);

            if(supplierViewModel is null)
                return NotFound();

            await _supplierRepository.DeleteAsync(id);
            
            return RedirectToAction(nameof(Index));
        }
        //[Route("update-address-supplier/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplierViewModel = await GetSupplierAddressAsync(id);

            if(supplierViewModel is null)
                return NotFound();

            return PartialView("_AddressUpdate", new SupplierViewModel { Address = supplierViewModel.Address });
        }

        //[Route("update-address-supplier/{id:guid}")]
        //[HttpPost]
        //public async Task<IActionResult> AtualizarEndereco(SupplierViewModel fornecedorViewModel)
        //{
        //    ModelState.Remove("Nome");
        //    ModelState.Remove("Documento");

        //    if (!ModelState.IsValid) return PartialView("_AddressUpdate", fornecedorViewModel);

        //    await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));

        //    if (!OperacaoValida()) return PartialView("_AtualizarEndereco", fornecedorViewModel);

        //    var url = Url.Action("ObterEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });
        //    return Json(new { success = true, url });
        //}
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
