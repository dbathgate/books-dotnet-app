using Microsoft.AspNetCore.Mvc;

namespace RazorApp.Controllers;

[ApiController]
[Route("/api/books")]
public class BookController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        
        return "Book 1";
    }
    
}