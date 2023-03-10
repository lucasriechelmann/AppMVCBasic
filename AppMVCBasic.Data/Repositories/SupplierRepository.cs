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
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AspMVCBasicDbContext db) : base(db)
        {
        }

        public async Task<Supplier> GetSupplierAddressAsync(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Supplier> GetSupplierProductsAddressAsync(Guid id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Products)
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
