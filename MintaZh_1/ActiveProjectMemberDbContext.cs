using Microsoft.EntityFrameworkCore;

namespace MintaZh_1
{
    public class ActiveProjectMemberDbContext : DbContext
    {
        /// <summary>
        /// <see cref="ActiveProjectMember"/> data.
        /// </summary>
        public DbSet<ActiveProjectMember> Members { get; set; }

        public ActiveProjectMemberDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //var connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;" +
                //    @"AttachDbFilename=|DataDirectory|\ActiveProjectMember.mdf;" +
                //    @"Integrated Security=True";

                optionsBuilder
                    .UseLazyLoadingProxies()
                    //.UseSqlServer(connStr);
                    .UseInMemoryDatabase("ActiveProjectMemberDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActiveProjectMember>()
                .HasKey(x => x.Name);
        }
    }
}
