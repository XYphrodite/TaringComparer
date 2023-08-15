using Microsoft.EntityFrameworkCore;
using TaringCompare.Models;

namespace TaringCompare.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Taring> Taring { get; set; }
        public ApplicationContext()
        {
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TaringStore;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
