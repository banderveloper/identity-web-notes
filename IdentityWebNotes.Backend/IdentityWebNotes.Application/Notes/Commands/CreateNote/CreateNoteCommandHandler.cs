using IdentityWebNotes.Application.Interfaces;
using IdentityWebNotes.Domain;
using MediatR;

namespace IdentityWebNotes.Application.Notes.Commands.CreateNote;

// CQRS handler for creating new note
public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private readonly INotesDbContext _context;

    public CreateNoteCommandHandler(INotesDbContext context)
    {
        _context = context;
    }

    // Creates new note. Accepts create note command with user id, title and details
    // returns Guid of new created note
    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        // Creating Note entity from request
        var note = new Note
        {
            UserId = request.UserId,
            Title = request.Title,
            Details = request.Details,

            Id = Guid.NewGuid(),
            CreationDate = DateTime.Now,
            EditDate = null
        };

        // adding to db
        _context.Notes.Add(note);
        await _context.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}