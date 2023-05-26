using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Repository;

namespace RazorApp.Pages;

public class BookNew : PageModel
{
    [BindProperty]
    public string? Name { get; set; }
    
    [BindProperty]
    public string? Author { get; set; }

    private BookDbContext _dbContext;
    
    public BookNew(BookDbContext bookDbContext)
    {
        _dbContext = bookDbContext;
    }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        var book = new Book { Name = Name, Author = Author };

        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();

        return new RedirectToPageResult("/Books");
    }
}