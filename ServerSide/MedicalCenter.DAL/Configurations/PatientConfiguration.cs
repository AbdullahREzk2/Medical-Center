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
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            // table Name 
            builder.ToTable("Patients");

            // PatientID 
            builder.HasKey(p => p.PatientID);
            builder.Property(p=>p.PatientID)
                .UseIdentityColumn(1,1);

            // firstName
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            // lastName
            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(30);

            // Phone
            builder.Property(p=>p.Phone)
                .IsRequired()
                .HasMaxLength(30);

            //Medical History
            builder.Property(p => p.MedicalHistory)
                .HasMaxLength(500);

            // Allergies 
            builder.Property(p => p.Allergies)
              .HasMaxLength(500);


            // User ID 
            builder.HasOne(p=>p.User)
                .WithOne(u=>u.Patient)
                .HasForeignKey<Patient>(p=>p.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
