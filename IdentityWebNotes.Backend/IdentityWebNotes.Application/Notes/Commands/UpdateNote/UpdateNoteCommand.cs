using MediatR;

namespace IdentityWebNotes.Application.Notes.Commands.UpdateNote;

// CQRS command for UpdateNoteCommand
public class UpdateNoteCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}