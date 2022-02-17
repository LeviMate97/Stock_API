using Microsoft.EntityFrameworkCore;

namespace Stock_API.Models
{
    public class PortfoliosDbContext : DbContext
    {
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<PurchaseHistory> PurchaseHistory { get; set; }
        public DbSet<PriceHistory> PriceHistory { get; set; }
        public DbSet<Dividends> Dividends { get; set; }
        public PortfoliosDbContext(DbContextOptions<PortfoliosDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Portfolio>().ToTable("Portfolio").HasNoKey();
            modelBuilder.Entity<PurchaseHistory>().ToTable("Purchase history").HasKey(k => new { k.Ticket, k.Quantity, k.Date });
            modelBuilder.Entity<PriceHistory>().ToTable("Price history").HasNoKey();
            modelBuilder.Entity<Dividends>().ToTable("Dividends").HasNoKey();
        }
    }
}