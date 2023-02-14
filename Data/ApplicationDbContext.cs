using invoice.Models;
using Microsoft.EntityFrameworkCore;

namespace invoice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Fee> Fees { get; set; } = default!;
        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<Invoice> Invoices { get; set; } = default!;
        public DbSet<ItemSold> ItemsSold { get; set; } = default!;
        public DbSet<Item> Items { get; set; } = default!;
        public DbSet<SalesReport> Reports { get; set; } = default!;
    }
}
