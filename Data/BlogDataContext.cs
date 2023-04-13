using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Server=localhost,1433;Database=Blog;User ID=sa;Password=Augusto92349923;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());
        }
    }
}