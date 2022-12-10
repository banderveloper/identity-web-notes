using IdentityWebNotes.Application.Interfaces;
using IdentityWebNotes.Domain;
using IdentityWebNotes.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebNotes.Persistence;

public class NotesDbContext : DbContext, INotesDbContext
{
    // From INotesDbContext
    public DbSet<Note> Notes { get; set; }

    // options are delivered to constructor for scoped DI and initializing connection string
    public NotesDbContext(DbContextOptions<NotesDbContext> options) 
        : base(options) {}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // NoteConfiguration - our entity type configuration
        modelBuilder.ApplyConfiguration(new NoteConfiguration());
    }
}