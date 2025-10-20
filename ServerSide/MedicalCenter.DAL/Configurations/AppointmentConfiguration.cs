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
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            // table Name
            builder.ToTable("Appointments");

            // Appointment ID
            builder.HasKey(a => a.AppointmentID);
            builder.Property(a => a.AppointmentID)
                .UseIdentityColumn(1, 1);

            // Appointment Date
            builder.Property(a => a.AppointmentDate)
                .IsRequired();

            // Status
            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>(); 
                


            // Notes
            builder.Property(a => a.Notes)
                .HasMaxLength(200);



            //Patient ID

            builder.HasOne(a=>a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientID)
                .OnDelete(DeleteBehavior.Restrict);


            // Doctor ID 
            builder.HasOne(a=>a.Doctor)
                .WithMany(d=>d.Appointments)
                .HasForeignKey(a=>a.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }
}
