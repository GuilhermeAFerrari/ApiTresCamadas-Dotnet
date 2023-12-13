﻿using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        // Some configurations
    }

    public DbSet<Supplier> Products { get; set; }
    public DbSet<Address> Adresses { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                .SelectMany(e => e.GetProperties()
                                .Where(p => p.ClrType == typeof(string)))
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
}