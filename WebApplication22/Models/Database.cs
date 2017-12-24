using Microsoft.EntityFrameworkCore;

namespace WebApplication22.Models
{
    public class Database: DbContext
    {
        //configure a sua conection
        protected const string ConnectionString = "Server=.\\SqlExpress;Database=DatabaseRazorPages;User Id=sa;Password=senha;MultipleActiveResultSets=true;";
        public Database()
        {

        }

        public DbSet<Credit> Credit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credit>()
                .ToTable("Credits");

            modelBuilder.Entity<Credit>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Credit>()
                .Property(x => x.Id)
                .UseSqlServerIdentityColumn()
                .IsRequired();

            modelBuilder.Entity<Credit>()
                .Property(x => x.Description)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
