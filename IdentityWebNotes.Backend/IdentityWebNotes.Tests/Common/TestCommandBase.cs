using IdentityWebNotes.Persistence;

namespace IdentityWebNotes.Tests.Common;

// Base class for classes to test COMMANDS
public class TestCommandBase : IDisposable
{
    protected readonly NotesDbContext Context;

    public TestCommandBase()
    {
        Context = NotesContextFactory.Create();
    }

    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }
}