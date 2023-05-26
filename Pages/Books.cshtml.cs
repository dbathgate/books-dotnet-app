using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Repository;

namespace RazorApp.Pages;

public class Books : PageModel
{
    public List<Book> BooksList { get; set; }

    private BookDbContext _dbContext;

    public Books(BookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnGet()
    {
        BooksList = _dbContext.Books.ToList();
    }
}