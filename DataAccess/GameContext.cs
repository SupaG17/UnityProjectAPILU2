using Microsoft.EntityFrameworkCore;
using ProjectMap.WebApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ProjectMap.WebApi.DataAccess
{
    public class GameContext : IdentityDbContext<IdentityUser>
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {
        }

        public DbSet<Environment2D> Environments { get; set; }

        public DbSet<GameObject> GameObjects { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Environment2D>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<GameObject>()
                .HasOne(g => g.Environment2D)
                .WithMany(e => e.GameObjects)
                .HasForeignKey(g => g.Environment2DId);
        }
    }

    }