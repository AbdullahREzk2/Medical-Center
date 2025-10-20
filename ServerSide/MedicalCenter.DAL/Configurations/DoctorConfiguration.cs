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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // Table Name 
            builder.ToTable("Doctors");

            // DoctorID
            builder.HasKey(d => d.DoctorID);
            builder.Property(d => d.DoctorID)
                .UseIdentityColumn(1, 1);

            //First Name 
            builder.Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(30);

            // Last Name
            builder.Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(30);

            //Phone
            builder.Property(d => d.Phone)
                .IsRequired()
                .HasMaxLength(20);


            //Specialization
            builder.Property(d => d.Specialization)
                .IsRequired();
               

            // Licence Number
            builder.Property(d => d.LicenceNumber)
                .IsRequired()
                .HasMaxLength(30);

            // Shift Hours
            builder.Property(d => d.ShiftHours)
                .IsRequired();

            // salary
            builder.Property(d => d.Salary)
               .IsRequired()
               .HasColumnType("decimal(18,2)");



            // UserId

            builder.HasOne(d=>d.User)
                .WithOne(u=>u.Doctor)
                .HasForeignKey<Doctor>(d=>d.UserID)
                .OnDelete(DeleteBehavior.Cascade);

        }



    }
}
