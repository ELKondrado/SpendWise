using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SpendWise_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpendWise_DataAccess.Configurations
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.ToTable("CartProduct");

            builder.HasKey(cp => new { cp.CartId, cp.ProductId });

            builder.Property(cp => cp.Quantity)
                .IsRequired();

            builder.Property(cp => cp.Price)
                .IsRequired();

            builder.HasOne(cp => cp.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CartId)
                .HasConstraintName("FK_CartProduct_Cart")
                .IsRequired();

            builder.HasOne(cp => cp.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(cp => cp.ProductId)
                .HasConstraintName("FK_CartProduct_Product")
                .IsRequired();
        }
    }
}
