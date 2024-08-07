using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nexer.Finance.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Nexer.Finance.Infrastructure.Configurations
{
    [ExcludeFromCodeCoverage]
    public class BillingConfiguration : IEntityTypeConfiguration<BillingEntity>
    {
        public void Configure(EntityTypeBuilder<BillingEntity> builder)
        {
            builder.ToTable("Billings");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
            .HasColumnType("uuid");

            builder.Property(b => b.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.CustomerId)
            .HasColumnType("uuid");

            builder.Property(b => b.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.Date)
                .IsRequired();

            builder.Property(b => b.DueDate)
                .IsRequired();

            builder.Property(b => b.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(b => b.Currency)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(b => b.CreatedAt)
                .IsRequired();

            builder.Property(b => b.UpdatedAt)
                .IsRequired(false);

            builder.HasOne(b => b.Customer)
            .WithMany()
            .HasForeignKey(c => c.CustomerId);

            builder.HasMany(b => b.Lines)
                .WithOne()
                .HasForeignKey(bl => bl.BillingId);

            
        }
    }
}
