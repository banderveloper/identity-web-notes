using IdentityWebNotes.Application.Common.Exceptions;
using IdentityWebNotes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebNotes.Application.Notes.Commands.UpdateNote;

// CQRS handler for updating existing note
public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
{
    private readonly INotesDbContext _context;

    public UpdateNoteCommandHandler(INotesDbContext context)
        => _context = context;
   
    // Accepts request than contains user and note id, and new title and details
    // than updates existing note. UNIT means than no value returns; 
    // throws NotFoundException if given note not found, or userid not equals
    public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        // Get note from db with id, given in the request
        var noteEntity = await _context.Notes.FirstOrDefaultAsync(
            note => note.Id == request.Id, cancellationToken);

        if (noteEntity is null || noteEntity.UserId != request.UserId)
            throw new NotFoundException(nameof(noteEntity), request.Id);

        // If entity found and user id equals, update note and save
        noteEntity.Title = request.Title;
        noteEntity.Details = request.Details;
        noteEntity.EditDate = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}