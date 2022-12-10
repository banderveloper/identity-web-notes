namespace IdentityWebNotes.Persistence;

public static class DbInitializer
{
    public static void Initialize(NotesDbContext context)
    {
        context.Database.EnsureCreated();
    }
}