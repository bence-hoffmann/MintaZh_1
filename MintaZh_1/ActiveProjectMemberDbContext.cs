using Microsoft.EntityFrameworkCore;

namespace MintaZh_1
{
    public class ActiveProjectMemberDbContext : DbContext
    {
        public DbSet<ActiveProjectMember> Members { get; set; }

        public ActiveProjectMemberDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                    @"AttachDbFilename=C:\Users\hoffm\source\repos\MintaZh_1\MintaZh_1\ActiveProjectMember.mdf;" +
                    @"Integrated Security=True";

                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveProjectMember>()
                .HasKey(x => x.Name);
        }
    }
}
