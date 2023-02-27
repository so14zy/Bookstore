using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Bookstore.DAL.Repositories;

namespace Bookstore.Controllers;

public class AuthorController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AuthorRepository _repository = null!;

    public AuthorController(ILogger<HomeController> logger, AuthorRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int? id)
    {
        if (id != null && id > 0)
        {
            Author author = await _repository.getByIdIncluded(Convert.ToInt32(id));
            
            return View("Views/Author/SingleAuthor.cshtml", author);
        }
        
        List<Author> authors = await _repository.getAllIncluded();
               
        return View(authors);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
