using MedicalCenter.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalCenter.DAL.Context
{
    public class MedicalCenterDbContext : DbContext
    {
        public MedicalCenterDbContext(DbContextOptions<MedicalCenterDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MedicalCenterDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Doctor> Doctors { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
