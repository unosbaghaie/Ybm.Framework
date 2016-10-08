using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ybm.Common.Models.Mapping
{
    public class TokenMap : EntityTypeConfiguration<Token>
    {
        public TokenMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.Name });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.AreaName)
                .HasMaxLength(16);

            this.Property(t => t.PersianName)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("Tokens");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.AreaName).HasColumnName("AreaName");
            this.Property(t => t.PersianName).HasColumnName("PersianName");
            this.Property(t => t.TokenCategory_Id).HasColumnName("TokenCategory_Id");
        }
    }
}
