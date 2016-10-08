using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ybm.Common.Models.Mapping
{
    public class UserGroupTokenMap : EntityTypeConfiguration<UserGroupToken>
    {
        public UserGroupTokenMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.UserGroup_Id, t.Token_Id });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UserGroup_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Token_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("UserGroupTokens");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserGroup_Id).HasColumnName("UserGroup_Id");
            this.Property(t => t.Token_Id).HasColumnName("Token_Id");
        }
    }
}
