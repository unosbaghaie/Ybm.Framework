using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ybm.Common.Models.Mapping
{
    public class ErrorLogMap : EntityTypeConfiguration<ErrorLog>
    {
        public ErrorLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Message)
                .IsRequired();

            this.Property(t => t.InnerException)
                .IsRequired();

            this.Property(t => t.StackTrace)
                .IsRequired();

            this.Property(t => t.Source)
                .IsRequired();

            this.Property(t => t.IpAddress)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ErrorLogs");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsCustomError).HasColumnName("IsCustomError");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.InnerException).HasColumnName("InnerException");
            this.Property(t => t.StackTrace).HasColumnName("StackTrace");
            this.Property(t => t.Source).HasColumnName("Source");
            this.Property(t => t.IpAddress).HasColumnName("IpAddress");
            this.Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            this.Property(t => t.ErrorType_Id).HasColumnName("ErrorType_Id");
            this.Property(t => t.User_Id).HasColumnName("User_Id");
        }
    }
}
