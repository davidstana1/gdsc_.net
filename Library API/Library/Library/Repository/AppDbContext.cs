﻿using Library.Models;
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
}