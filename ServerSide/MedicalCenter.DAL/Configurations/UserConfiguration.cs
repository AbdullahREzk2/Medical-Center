using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalCenter.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCenter.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // table name
            builder.ToTable("Users");

            // User ID
            builder.HasKey(u => u.UserID);
            builder.Property(u => u.UserID)
                .UseIdentityColumn(1,1);

            // National ID
            builder.Property(u=>u.NationalID)
                .IsRequired()
                .HasMaxLength(14);

            builder.HasIndex(u => u.NationalID)
                .IsUnique();

            // Gender 
            builder.Property(u => u.Gender)
                .IsRequired()
                .HasConversion<string>();


            // DOB 
            builder.Property(u => u.DOB)
                .IsRequired();

            // Email 
            builder.HasIndex(u=>u.Email)
                .IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            // Password 
            builder.Property(u => u.PasswordHash)
               .IsRequired()
               .HasMaxLength(255);

            // Role 
            builder.Property(u => u.Role)
              .IsRequired()
              .HasConversion<string>();


            // IsActive 
            builder.Property(u => u.IsActive)
              .IsRequired()
              .HasDefaultValue(true);

            // Created At 
            builder.Property(u => u.CreatedAt)
           .IsRequired()
           .HasDefaultValueSql("GETUTCDATE()");

            // LastLogin 
            builder.Property(u => u.LastLogin)
                .IsRequired(false);


          
            // Relationships
            builder.HasOne(u => u.Patient)
                   .WithOne(p => p.User)
                   .HasForeignKey<Patient>(p => p.UserID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Doctor)
                   .WithOne(d => d.User)
                   .HasForeignKey<Doctor>(d => d.UserID)
                   .OnDelete(DeleteBehavior.Cascade);
           


        }


    }
}
