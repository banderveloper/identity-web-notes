using MediatR;

namespace IdentityWebNotes.Application.Notes.Commands.CreateNote;

// CQRS command, used for CreateNoteCommandHandler, returns Guid of created note
public class CreateNoteCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}