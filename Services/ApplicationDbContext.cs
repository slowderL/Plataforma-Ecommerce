using Microsoft.EntityFrameworkCore;
using BestStoreMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace BestStoreMVC.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
