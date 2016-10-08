using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Ybm.Common.Models.Mapping
{
    public class PageMap : EntityTypeConfiguration<Page>
    {
        public PageMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.PageName, t.PageHref, t.IsVisible, t.UserGroup_Id, t.CreationDateTime, t.Priority, t.SelectorCssClass, t.PageScope_Id });

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PageName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PageHref)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.UserGroup_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Priority)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SelectorCssClass)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PageScope_Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Pages");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PageName).HasColumnName("PageName");
            this.Property(t => t.PageHref).HasColumnName("PageHref");
            this.Property(t => t.IsVisible).HasColumnName("IsVisible");
            this.Property(t => t.Parent_Id).HasColumnName("Parent_Id");
            this.Property(t => t.UserGroup_Id).HasColumnName("UserGroup_Id");
            this.Property(t => t.CreationDateTime).HasColumnName("CreationDateTime");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.SelectorCssClass).HasColumnName("SelectorCssClass");
            this.Property(t => t.PageScope_Id).HasColumnName("PageScope_Id");
        }
    }
}
