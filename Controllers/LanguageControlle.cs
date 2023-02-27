using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Bookstore.DAL.Repositories;

namespace Bookstore.Controllers;

public class LanguageController : Controller
{
    private readonly ILogger<HomeController> _logger;
    LanguageRepository _repository = null!;

    public LanguageController(ILogger<HomeController> logger, LanguageRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IActionResult> Index(int? id)
    {
        if (id != null && id > 0)
        {
            Language language = await _repository.getByIdIncluded(Convert.ToInt32(id));
            
            return View("Views/Language/SingleLanguage.cshtml", language);
        }

        List<Language> languages = await _repository.getAllIncluded();

        return View(languages);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
