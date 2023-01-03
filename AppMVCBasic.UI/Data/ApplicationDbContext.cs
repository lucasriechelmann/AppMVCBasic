using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppMVCBasic.UI.Models;

namespace AppMVCBasic.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppMVCBasic.UI.Models.AddressViewModel> AddressViewModel { get; set; }
    }
}