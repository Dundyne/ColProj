using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Collective> Collectives { get; set; }
        public DbSet<CollectiveUser> CollectiveUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
            .HasOne(p => p.Collective)
            .WithMany(k => k.Posts);

            modelBuilder.Entity<CollectiveUser>()
    .HasKey(bc => new { bc.UserId, bc.CollectiveId });
            modelBuilder.Entity<CollectiveUser>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.CollectiveUsers)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<CollectiveUser>()
                .HasOne(bc => bc.Collective)
                .WithMany(c => c.CollectiveUsers)
                .HasForeignKey(bc => bc.CollectiveId);


                }
    }
}
