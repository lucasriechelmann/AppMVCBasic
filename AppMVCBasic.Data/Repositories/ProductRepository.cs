using AppMVCBasic.Business.Interfaces;
using AppMVCBasic.Business.Models;
using AppMVCBasic.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMVCBasic.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AspMVCBasicDbContext db) : base(db)
        {
        }

        public async Task<List<Product>> GetProductsBySupplierAsync(Guid supplierId)
        {
            return await GetAsync(x => x.SupplierId == supplierId);
        }

        public async Task<List<Product>> GetProductsSuppliersAsync()
        {
            return await DbSet.AsNoTracking().Include(x => x.Supplier).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Product> GetProductSupplierAsync(Guid id)
        {
            return await DbSet.AsNoTracking().Include(x => x.Supplier).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
