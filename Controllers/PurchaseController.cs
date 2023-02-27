using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models.ViewModels;

namespace Bookstore.Controllers;

public class PurchaseController : Controller
{
    [HttpPost]
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
