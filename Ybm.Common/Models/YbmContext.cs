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

        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
