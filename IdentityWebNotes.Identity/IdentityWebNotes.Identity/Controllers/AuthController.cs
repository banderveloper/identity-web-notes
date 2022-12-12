using IdentityServer4.Services;
using IdentityWebNotes.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWebNotes.Identity.Controllers;

public class AuthController : Controller
{
    // Identity classes
    // SignInManager - for user login
    private readonly SignInManager<AppUser> _signInManager;
    // for searching user
    private readonly UserManager<AppUser> _userManager;
    // for logout
    private readonly IIdentityServerInteractionService _serverInteraction;

    public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService serverInteraction)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _serverInteraction = serverInteraction;
    }

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
    public async Task<IActionResult> Login(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        // Find user by name in db
        var user = await _userManager.FindByNameAsync(viewModel.Username);
        
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(viewModel);
        }
        
        // Try to login
        // Third bool parameter is IsPersistent - persistent cookie, when we close browser but they are saved and relogin
        // Fourth bool - lockout on failure
        var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);

        if (result.Succeeded)
        {
            return Redirect(viewModel.ReturnUrl);
        }
        
        ModelState.AddModelError(string.Empty, "Login error");
        
        return View(viewModel);
    }
}