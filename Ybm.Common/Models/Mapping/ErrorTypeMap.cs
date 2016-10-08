using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ybm.Common.Models.Mapping
{
    public class ErrorTypeMap : EntityTypeConfiguration<ErrorType>
    {
        public ErrorTypeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.Name });

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ErrorTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
