using IdentityWebNotes.Application.Common.Exceptions;
using IdentityWebNotes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebNotes.Application.Notes.Commands.DeleteNote;

// CQRS handler for deleting existing note
public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly INotesDbContext _context;

    public DeleteNoteCommandHandler(INotesDbContext context)
        => _context = context;
    
    
    // Accepts request than contains user and note id
    // And deletes note with given id if note exists and userid equals
    // Throws NotFoundException if note not found or user id not equals
    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        // Gets note entity from db
        var noteEntity = await _context.Notes.FirstOrDefaultAsync(
            note => note.Id == request.Id, cancellationToken);

        if (noteEntity is null || noteEntity.UserId != request.UserId)
            throw new NotFoundException(nameof(noteEntity), request.Id);
        
        // If entity found and userId equals - deleting note
        _context.Notes.Remove(noteEntity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}