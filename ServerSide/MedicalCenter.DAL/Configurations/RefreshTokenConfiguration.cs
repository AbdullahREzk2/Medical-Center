using MedicalCenter.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedicalCenter.DAL.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // Table name (optional)
            builder.ToTable("RefreshTokens");

            // Primary key
            builder.HasKey(rt => rt.Id);

            // Relationships
            builder.HasOne(rt => rt.User)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(rt => rt.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Property configurations
            builder.Property(rt => rt.Token)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(rt => rt.ExpiryDate)
                   .IsRequired();

            builder.Property(rt => rt.CreationDate)
                   .IsRequired();

            builder.Property(rt => rt.Revoked)
                   .IsRequired(false);
        }
    }
}
