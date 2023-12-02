using Library.Models;
using Library.Models.Genres;
using Microsoft.EntityFrameworkCore;

namespace Library.Repository;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }

    public DbSet<Books> BooksEnumerable { get; set; }
    public DbSet<Genre> Genres { get; set; }
}