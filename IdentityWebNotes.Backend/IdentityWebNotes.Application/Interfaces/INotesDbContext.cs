using IdentityWebNotes.Domain;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebNotes.Application.Interfaces;

// Interface for main db context
// Application (mediatR in our case) will use only interface for communicating with db
// So we need at least Notes DbSet and SaveChanges function for making changes in db
// Implementation NotesDbContext created at Persistence level and injected to mediatR by DI
public interface INotesDbContext
{
    DbSet<Note> Notes { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}