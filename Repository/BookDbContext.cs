using Microsoft.EntityFrameworkCore;

namespace RazorApp.Repository;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Book>? Books { get; set; }
}