using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryControlSystem.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }


        ////public DbSet<Fund> Funds { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>().ToTable("Products");
        //    modelBuilder.Entity<User>().ToTable("Users");
        //    modelBuilder.Entity<Customer>().ToTable("Customers");
        //    modelBuilder.Entity<Supplier>().ToTable("Suppliers");
        //    modelBuilder.Entity<Order>().ToTable("Orders");
        //    modelBuilder.Entity<OrderList>().ToTable("OrderLists");

            
        //}

    }
}
