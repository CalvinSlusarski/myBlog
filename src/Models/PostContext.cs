using Microsoft.EntityFrameworkCore;
using Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Code.Models;

namespace Code.Models
{
    public class PostsContext : DbContext
    {
        public PostsContext() { }
        public PostsContext(DbContextOptions<PostsContext> options)
            : base(options)
        { }

        public DbSet<Post> Post { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
