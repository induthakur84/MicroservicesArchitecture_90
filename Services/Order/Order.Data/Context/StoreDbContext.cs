using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Data.Context
{
    public class StoreDbContext:DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);
        }


        public DbSet<User> Users { get; set; }
    }
}
