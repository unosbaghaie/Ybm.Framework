using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ybm.Common.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.MobileNumber)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.ActivationCode)
                .IsRequired()
                .HasMaxLength(16);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserGroup_Id).HasColumnName("UserGroup_Id");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            this.Property(t => t.Reputation).HasColumnName("Reputation");
            this.Property(t => t.LockedCredit).HasColumnName("LockedCredit");
            this.Property(t => t.IsActivated).HasColumnName("IsActivated");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.ActivationCode).HasColumnName("ActivationCode");
            this.Property(t => t.UserHashKey).HasColumnName("UserHashKey");
            this.Property(t => t.IsVerified).HasColumnName("IsVerified");
            this.Property(t => t.VerificationDateTime).HasColumnName("VerificationDateTime");
            this.Property(t => t.IsFirstLogin).HasColumnName("IsFirstLogin");

            // Relationships
            this.HasRequired(t => t.UserGroup)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.UserGroup_Id);

        }
    }
}
