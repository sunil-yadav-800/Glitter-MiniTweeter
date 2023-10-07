using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class DataBaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DataBaseContext()
        { }
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }
        
        public Microsoft.EntityFrameworkCore.DbSet<User> users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Follower> followers { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Tweet> tweets { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Like> likes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follower>()
                    .HasOne(f => f.follower)
                    .WithMany(u => u.followerrr)
                    .HasForeignKey(f => f.FollowerId)
                    .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Follower>()
                        .HasOne(f => f.followee)
                        .WithMany(u => u.followeeee)
                        .HasForeignKey(f => f.FolloweeId)
                        .OnDelete(DeleteBehavior.ClientCascade);

            
        }
        
    }
}
