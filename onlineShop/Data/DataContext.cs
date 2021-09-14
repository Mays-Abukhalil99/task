using Microsoft.EntityFrameworkCore;
using onlineShop.Entity;


namespace onlineShop.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CartEntity> Carts { get; set; }
        public DbSet<InventoryEntity> Inventories { get; set; }

    }



}

