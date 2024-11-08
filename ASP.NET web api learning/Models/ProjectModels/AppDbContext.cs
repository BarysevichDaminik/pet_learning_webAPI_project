using ASP.NET_web_api_learning.models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ASP.NET_web_api_learning.models.ProjectModels
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<Credentials> Credentials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Credentials>(entity =>
            {
                entity.ToTable("Credentials");
                entity.HasKey(e => e.id);
                entity.Property(e => e.username).IsRequired();
                entity.Property(e => e.password).IsRequired();
            });
        }
    }
}
