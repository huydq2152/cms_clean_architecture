using System.Reflection;
using CleanArchitecture.Domain.Entities.Auth;
using CleanArchitecture.Domain.Entities.Posts;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PostCategory> PostCategories => Set<PostCategory>();
        public DbSet<Post> Posts => Set<Post>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName != null && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName($"Identity_{tableName[6..]}");
                }
            }
        }
    }
}