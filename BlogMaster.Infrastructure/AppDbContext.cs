using BlogMaster.Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogMaster.Infrastructure;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Article> Article { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<Tag> Tag { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<ArticleVote>ArticleVote { get; set; }
    public DbSet<BlackList>BlackList { get; set; }
}