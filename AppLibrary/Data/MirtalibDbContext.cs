using AppLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace AppLibrary.Data
{
    public class MirtalibDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //lokal
            object value = optionsBuilder.UseSqlServer("Data Source=DESKTOP-U9UFRFT\\SQLEXPRESS;Initial Catalog=StepAppDb;User ID=sa;Password=258025;Trusted_Connection=True;TrustServerCertificate=True;");
           
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LikedItem> LikedItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CreditCard> CreditCarts { get; set; }
        public DbSet<PhotoProduct> PhotoProducts { get; set; }
        public DbSet<PhotoUser> PhotoUsers { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             

            modelBuilder.Entity<PhotoProduct>()
                .Property(p => p.Size)
                .HasPrecision(18, 2);  
            modelBuilder.Entity<PhotoUser>()
                .Property(p => p.Size)
                .HasPrecision(18, 2);   

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
            modelBuilder.Entity<CreditCard>().HasData(
                new CreditCard
                {
                    UserId= 8,
                    money=100000,
                    Number = "4169738849092501",
                    ExpirationDate=new System.DateTime(2026,01,10),
                    CVV= "576",
                }
                );
            modelBuilder.Entity<Admin>().HasData(
              new Admin
              {
                  Id=1,
                  Email = "mqul21810@gmail.com",
                  FirstName = "Miri",
                  LastName = "Emirli",
                  Password = PasswordHasher.HashPassword("admin12")  
              }
            );

            modelBuilder.Entity<Product>()
                .HasMany(p => p.CartItems)
                .WithOne(cp => cp.Product);

            modelBuilder.Entity<Cart>()
                .HasMany(p => p.Items)
                .WithOne(cp => cp.Cart);

            modelBuilder.Entity<Product>()
           .HasOne(p => p.Category)
           .WithMany(c => c.Products)
           .HasForeignKey(p => p.CategoryId);
             
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.ProductId, oi.OrderId });
        }

    }
}
