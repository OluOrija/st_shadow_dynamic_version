namespace stshadowbackend.Data
{
    using Microsoft.EntityFrameworkCore;
    using stshadowbackend.Models;
    using System.Collections.Generic;
    using System.Reflection.Emit;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<NavigationMenu> NavigationMenus { get; set; }
        public DbSet<SectionContent> SectionContents { get; set; }
        public DbSet<MediaAssets> MediaAssets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Add indexes for performance
            modelBuilder.Entity<NavigationMenu>().HasIndex(n => n.Order);
            modelBuilder.Entity<SectionContent>().HasIndex(s => s.Order);
        }
    }

}
