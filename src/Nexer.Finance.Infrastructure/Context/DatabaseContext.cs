using Microsoft.EntityFrameworkCore;
using Nexer.Finance.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Nexer.Finance.Infrastructure.Context
{
    [ExcludeFromCodeCoverage]
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<BillingEntity> Billings => Set<BillingEntity>();
        public DbSet<BillingLineEntity> BillingLines => Set<BillingLineEntity>();
    }
}
