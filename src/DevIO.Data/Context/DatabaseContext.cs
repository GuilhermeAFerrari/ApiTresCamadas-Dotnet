using DevIO.Business.Models;
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
}