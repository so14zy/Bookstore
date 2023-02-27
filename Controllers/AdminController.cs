using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Bookstore.DAL;
namespace Bookstore.Controllers;

[Authorize(Roles="Admin")]
public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DatabaseApplicationContext _repository = null!;

    public AdminController(ILogger<HomeController> logger, DatabaseApplicationContext repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    public async Task<IActionResult> AddBook()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(VMBook vmBook)
    {
        Book? tempBook = await _repository.Books
            .Where(u => u.Title == vmBook.Title)
            .FirstOrDefaultAsync();

        if (tempBook != null)
        {
            ViewData["ValidateMessage"] = "Such a book already exists";
            return View();
        }

        Author? tempAuthor = await _repository.Authors
            .Where(u => u.SecondName == vmBook.SecondName)
            .FirstOrDefaultAsync();
        
        if (tempAuthor == null)
        {
            ViewData["ValidateMessage"] = "No such author";
            return View();
        }

        Genre? tempGenre = await _repository.Genres
            .Where(u => u.Name == vmBook.Genre)
            .FirstOrDefaultAsync();

        if (tempGenre == null)
        {
            ViewData["ValidateMessage"] = "No such genre";
            return View();
        }

        Language? tempLanguage = await _repository.Languages
            .Where(u => u.Name == vmBook.Language)
            .FirstOrDefaultAsync();

        if (tempLanguage == null)
        {
            ViewData["ValidateMessage"] = "No such language";
            return View();
        }

        tempBook = new Book();
        tempBook.Title = vmBook.Title;
        tempBook.AuthorId = tempAuthor.Id;
        tempBook.GenreId = tempGenre.Id;
        
        BookInfo info = new BookInfo();
        info.BookId = tempBook.Id;
        info.DateWritten = vmBook.DateWritten;
        info.IsAvailable = vmBook.IsAvailable;
        info.Price = vmBook.Price;
        info.LanguageId = tempLanguage.Id;

        tempBook.Info = info;

        _repository.Books.Add(tempBook);
        _repository.SaveChanges();

        return RedirectToAction("Index", "Book", new { id = tempBook.Id });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
