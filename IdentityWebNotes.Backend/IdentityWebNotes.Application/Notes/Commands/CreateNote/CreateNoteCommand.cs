using MediatR;

namespace IdentityWebNotes.Application.Notes.Commands.CreateNote;

// CQRS command for CreateNoteCommandHandler
public class CreateNoteCommand : IRequest<Guid>
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}