using Microsoft.EntityFrameworkCore;
using RandomUser.Business.Entity.Configuration;
using RandomUser.Business.Model;
using System;

namespace RandomUser.Business.Entity
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<User> Users { get; set; }
    }
}
