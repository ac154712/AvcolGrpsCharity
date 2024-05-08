using AvcolGrpsCharity.Models;
using Microsoft.EntityFrameworkCore;

namespace AvcolGrpsCharity.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<SignedCharityGrps> SignedCharityGrps { get; set; }
        public DbSet<CharityGrpStaff> CharityGrpStaff { get; set; }
        public DbSet<Donors> Donors { get; set; }
        public DbSet<Donations> Donations { get; set; }
        public DbSet<CharityCategory> CharityCategory { get; set; }
    }
}