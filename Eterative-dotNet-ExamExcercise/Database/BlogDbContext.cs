using Eterative_dotNet_ExamExcercise.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eterative_dotNet_ExamExcercise.Database
{
    public class BlogDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<Blog> Blog { get; set; }
        public DbSet<RelatedBlogs> RelatedBlogs { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options, IConfiguration config) : base(options)
        {
            _config = config; 
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("TestDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelatedBlogs>()
                .HasKey(e => new { e.BaseBlog, e.RelatedTo });

            modelBuilder.Entity<RelatedBlogs>()
                .HasOne(e => e.BaseBlogObj)
                .WithMany(e => e.RelatedBlogs)
                .HasForeignKey(e => e.BaseBlog)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RelatedBlogs>()
                .HasOne(e => e.RelatedToObj)
                .WithMany(e => e.RelatedToBlogs)
                .HasForeignKey(e => e.RelatedTo)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
