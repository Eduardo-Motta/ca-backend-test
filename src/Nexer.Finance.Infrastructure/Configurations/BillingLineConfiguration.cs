using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexer.Finance.Domain.Entities;

namespace Nexer.Finance.Infrastructure.Configurations
{
    public class BillingLineConfiguration : IEntityTypeConfiguration<BillingLineEntity>
    {
        public void Configure(EntityTypeBuilder<BillingLineEntity> builder)
        {
            builder.ToTable("BillingLines");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasColumnType("uuid");

            builder.Property(b => b.BillingId)
                .HasColumnType("uuid");

            builder.Property(b => b.ProductId)
                .HasColumnType("uuid");

            builder.Property(b => b.Quantity)
                .IsRequired();

            builder.Property(b => b.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(b => b.Subtotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(b => b.UpdatedAt)
                .IsRequired(false);

            builder.HasOne<BillingEntity>()
                .WithMany(b => b.Lines)
                .HasForeignKey(b => b.BillingId);

            builder.HasOne(b => b.Product)
            .WithMany()
            .HasForeignKey(c => c.ProductId);
        }
    }
}
