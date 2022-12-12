using IdentityWebNotes.Identity.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWebNotes.Identity.Controllers;

public class AuthController : Controller
{
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel()
        {
            ReturnUrl = returnUrl
        };
        return View(viewModel);
    }

    
    [HttpPost]
    public IActionResult Login(LoginViewModel viewModel)
    {
        return View(viewModel);
    }
}