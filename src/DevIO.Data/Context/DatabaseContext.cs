using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        // By default EF observes the returned instance of the bank, this setting desable this behavor
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Supplier> Products { get; set; }
    public DbSet<Address> Adresses { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                .SelectMany(e => e.GetProperties()
                                .Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(100)");
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                                    .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegistrationDate") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("DataCadastro").IsModified = false;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}