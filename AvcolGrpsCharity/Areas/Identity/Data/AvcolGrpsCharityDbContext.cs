using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AvcolGrpsCharity.Models;
using AvcolGrpsCharity;

namespace AvcolGrpsCharity.Areas.Identity.Data;

public class AvcolGrpsCharityDbContext : IdentityDbContext<IdentityUser>
{
    public AvcolGrpsCharityDbContext(DbContextOptions<AvcolGrpsCharityDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<AvcolGrpsCharity.Models.SignedCharityGrps> SignedCharityGrps { get; set; } = default!;

public DbSet<AvcolGrpsCharity.Models.Donors> Donors { get; set; } = default!;

public DbSet<AvcolGrpsCharity.Models.Donations> Donations { get; set; } = default!;

public DbSet<AvcolGrpsCharity.Models.CharityGrpStaff> CharityGrpStaff { get; set; } = default!;

public DbSet<AvcolGrpsCharity.Models.CharityCategory> CharityCategory { get; set; } = default!;
}
