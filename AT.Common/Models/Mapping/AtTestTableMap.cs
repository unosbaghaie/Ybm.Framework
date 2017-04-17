using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace AT.Common.Models.Mapping
{
    public class AtTestTableMap : EntityTypeConfiguration<AtTestTable>
    {
        public AtTestTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.FirstName)
                .HasMaxLength(32);

            this.Property(t => t.LastName)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("AtTestTable");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");


        }
    }
}
