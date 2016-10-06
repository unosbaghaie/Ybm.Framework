using System.ComponentModel.DataAnnotations;
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
                .HasMaxLength(32);

            this.Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.PhoneNumber)
                .IsRequired()
                .HasMaxLength(16);

            // Table & Column Mappings
            this.ToTable("Users");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
        }
    }
}
