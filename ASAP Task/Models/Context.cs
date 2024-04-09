using Microsoft.EntityFrameworkCore;

namespace ASAP_Task.Models
{
    public class Context :DbContext
    {
        public Context() { }
        public Context (DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>()
                .HasIndex(c => c.Email)
                .IsUnique();
        }
        public virtual DbSet<Clients> Clients { get; set; }
        public DbSet<StockMarketData> StockMarketData { get; set; }

    }
}
