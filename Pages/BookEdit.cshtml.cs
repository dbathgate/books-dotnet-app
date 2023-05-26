using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorApp.Repository;

namespace RazorApp.Pages;

public class BookEdit : PageModel
{
    [BindProperty(SupportsGet = true)]
    public string? Id { get; set; }

    public Book? book { get; set; }

    [BindProperty]
    public string? Name { get; set; }
    
    [BindProperty]
    public string? Author { get; set; }
    
    private BookDbContext _dbContext;

    public BookEdit(BookDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IActionResult OnGet()
    {
        Console.WriteLine(Id);

        book = _dbContext.Books.FirstOrDefault(b => b.Id.ToString() == Id);

        if (book == null)
        {
            return NotFound();
        }

        return null;
    }

    public IActionResult OnPost()
    {
        var updatedBook = new Book
        {
            Id = Guid.Parse(Id),
            Author = Author,
            Name = Name
        };

        _dbContext.Update(updatedBook);
        _dbContext.SaveChanges();

        return RedirectToPage("/Books");
    }

    public IActionResult OnPostDelete()
    {
        _dbContext.Books.Remove(new Book { Id = Guid.Parse(Id) });
        _dbContext.SaveChanges();

        return RedirectToPage("/Books");
    }
}