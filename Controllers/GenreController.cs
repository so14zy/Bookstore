using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Bookstore.DAL.Repositories;

namespace Bookstore.Controllers;

public class GenreController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly GenreRepository _repository;

    public GenreController(ILogger<HomeController> logger, GenreRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IActionResult> Index(int? id)
    {
        if (id != null && id > 0)
        {
            Genre genre = await _repository.getByIdIncluded(Convert.ToInt32(id));
            
            return View("Views/Genre/SingleGenre.cshtml", genre);
        }

        List<Genre> genres = await _repository.getAllIncluded();
        
        return View(genres);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
