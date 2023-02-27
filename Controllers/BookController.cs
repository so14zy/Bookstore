using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Bookstore.DAL.Repositories;

namespace Bookstore.Controllers;

public class BookController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookRepository _repository = null!;

    public BookController(ILogger<HomeController> logger, BookRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }


    public async Task<IActionResult> Index(int? id)
    {
        if (id != null && id > 0)
        {
            Book book = await _repository.getByIdIncluded(Convert.ToInt32(id));
            
            return View("Views/Book/SingleBook.cshtml", book);
        }
        
        List<Book> books = await _repository.getAllIncluded();
                       
        return View(books);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
