
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;

namespace ProductManagement.Data
{
    public class DataContextEF : IdentityDbContext
    {
        // in the Udemy Course The Instructor didn't use DependecyInjection and also Identity framework for authentication and role management so this was working fine but Now if i want to add identity Framework i want to remove this and rely on the program.cs centreialized approach so i need to configure the dbcontext in the program.cs , thats why i commenting this out

        /* private readonly IConfiguration _config;

        public DataContextEF(IConfiguration config) {
            _config = config;
        } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            if(!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(
                    _config.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure()
                );
                
            }

        } */

        public DataContextEF(DbContextOptions<DataContextEF> options) : base(options) { }

        public virtual DbSet<Category>? Categories { get; set; }
        public virtual DbSet<Product>? Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method to configure Identity entities
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .ToTable("Categories")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category) // Each product has one category
                .WithMany(c => c.Products) // Each category has many products

                .HasForeignKey(p => p.CategoryId) // Foreign key in Product table
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete

           /*  modelBuilder.Entity<Product>()
                .HasOne(p => p.User) // Each Product has one category
                .WithMany(u => u.Products) // Each User Has many products
                .HasForeignKey(p => p.UserId) // Foreign Key in User Table
                .OnDelete(DeleteBehavior.Cascade); */

        }
    }
}