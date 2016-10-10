using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Ybm.Common.Models.Mapping;

namespace Ybm.Common.Models
{
    public partial class YbmContext : DbContext
    {
        static YbmContext()
        {
            Database.SetInitializer<YbmContext>(null);
        }

        public YbmContext()
            : base("Name=YbmContext")
        {
        }

        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<ErrorType> ErrorTypes { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<TokenCategory> TokenCategories { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserGroupToken> UserGroupTokens { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AspNetRoleMap());
            modelBuilder.Configurations.Add(new AspNetUserClaimMap());
            modelBuilder.Configurations.Add(new AspNetUserLoginMap());
            modelBuilder.Configurations.Add(new AspNetUserMap());
            modelBuilder.Configurations.Add(new ErrorLogMap());
            modelBuilder.Configurations.Add(new ErrorTypeMap());
            modelBuilder.Configurations.Add(new PageMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TokenCategoryMap());
            modelBuilder.Configurations.Add(new TokenMap());
            modelBuilder.Configurations.Add(new UserGroupMap());
            modelBuilder.Configurations.Add(new UserGroupTokenMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
