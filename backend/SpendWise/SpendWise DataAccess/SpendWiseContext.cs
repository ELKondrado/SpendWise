﻿using Microsoft.EntityFrameworkCore;
using SpendWise_DataAccess.Configurations;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess
{
    public class SpendWiseContext : DbContext
    {
        public SpendWiseContext(DbContextOptions<SpendWiseContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new CartConfiguration().Configure(modelBuilder.Entity<Cart>());
            new CartProductConfiguration().Configure(modelBuilder.Entity<CartProduct>());
        }
    }
}
