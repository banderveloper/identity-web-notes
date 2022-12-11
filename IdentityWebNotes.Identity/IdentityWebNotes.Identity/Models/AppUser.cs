using Microsoft.AspNetCore.Identity;

namespace IdentityWebNotes.Identity.Models;

// Custom identity user, added first and last name
public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}