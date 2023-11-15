using Microsoft.EntityFrameworkCore;
using Fundipedia.TechnicalInterview.Model.Supplier;

namespace Fundipedia.TechnicalInterview.Data.Context;

public class SupplierContext : DbContext
{
    public SupplierContext(DbContextOptions<SupplierContext> options)
        : base(options)
    {
    }

    public DbSet<Supplier> Suppliers { get; set; }

    public DbSet<Email> Emails { get; set; }

    public DbSet<Phone> Phones { get; set; }
}