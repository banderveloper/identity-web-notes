namespace IdentityWebNotes.Identity.Data;

public static class DbInitializer
{
    public static void Initialize(AuthDbContext context)
    {
        context.Database.EnsureCreated();
    }
}