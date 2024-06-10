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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product").HasKey(p => p.Id);

            builder.Property(p => p.Name).HasMaxLength(255);

            builder.HasMany(p => p.Categories).WithMany(c => c.Products)
                .UsingEntity<Dictionary<string, object>>("ProductCategory",
                    e => e.HasOne<Category>()
                            .WithMany()
                            .HasForeignKey("CategoryId")
                            .HasConstraintName("FN_ProductCategory_Category"),
                    e => e.HasOne<Product>()
                            .WithMany()
                            .HasForeignKey("ProductId")
                            .HasConstraintName("FN_ProductCategory_Product"));
        }
    }
}
