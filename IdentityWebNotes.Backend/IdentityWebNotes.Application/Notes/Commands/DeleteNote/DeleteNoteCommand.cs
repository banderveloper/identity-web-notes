using MediatR;

namespace IdentityWebNotes.Application.Notes.Commands.DeleteNote;

// CQRS command for DeleteNoteCommandHandler
public class DeleteNoteCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}