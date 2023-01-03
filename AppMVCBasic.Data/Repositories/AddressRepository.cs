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
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(AspMVCBasicDbContext db) : base(db)
        {
        }

        public async Task<Address> GetAddressBySupplierAsync(Guid supplierId)
        {
            return await DbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.SupplierId == supplierId);
        }
    }
}
