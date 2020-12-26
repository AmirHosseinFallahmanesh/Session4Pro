using Microsoft.EntityFrameworkCore;

namespace Demo.Dal
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Query<IranOrderDTO>().ToView("OrderFromIranList");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        public DbQuery<IranOrderDTO> IraninList { get; set; }
    }
}
