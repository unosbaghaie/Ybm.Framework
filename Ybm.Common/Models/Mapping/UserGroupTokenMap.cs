using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ybm.Common.Models.Mapping
{
    public class UserGroupTokenMap : EntityTypeConfiguration<UserGroupToken>
    {
        public UserGroupTokenMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UserGroupTokens");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserGroup_Id).HasColumnName("UserGroup_Id");
            this.Property(t => t.Token_Id).HasColumnName("Token_Id");

            // Relationships
            this.HasRequired(t => t.Token)
                .WithMany(t => t.UserGroupTokens)
                .HasForeignKey(d => d.Token_Id);
            this.HasRequired(t => t.UserGroup)
                .WithMany(t => t.UserGroupTokens)
                .HasForeignKey(d => d.UserGroup_Id);

        }
    }
}
