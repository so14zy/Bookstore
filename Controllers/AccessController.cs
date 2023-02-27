using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;
using Bookstore.Models.ViewModels;
using Bookstore.DAL.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Bookstore.Controllers;

public class AccessController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserRepository _repository = null!;

    public AccessController(ILogger<HomeController> logger, UserRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;

        if (claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(VMLogin modelLogin)
    {
        User userToAdd = new User();
        User user = await _repository.getByEmail(modelLogin.Email);


        if (user == null)
        {
            user = new User();
            user.Email = modelLogin.Email;
            user.Password = modelLogin.Password;
            user.RoleId = 2;
        }
        else if (modelLogin.Password != user.Password)
        {
            ViewData["ValidateMessage"] = "wrong password";
            return View();
        }

        
        _repository.add(user);
        user = await _repository.getByEmail(modelLogin.Email);

        List<Claim> claims = new List<Claim>() {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Email
                ),
            new Claim(
                ClaimTypes.Role,
                user.Role.Name
                )
        };

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        AuthenticationProperties properties = new AuthenticationProperties() {
            AllowRefresh = true,
            IsPersistent = modelLogin.KeepLoggedIn
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            properties
            );

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Access");
    }

    public async Task<IActionResult> Block()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}