using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication22.Models;
namespace WebApplication22.Models
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            //this.Database.EnsureCreated();
        }
        public DbSet<WebApplication22.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
