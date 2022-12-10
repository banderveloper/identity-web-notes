using MediatR;

namespace IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;

// Query for GetNoteDetailsQueryHandler
public class GetNoteDetailsQuery : IRequest<NoteDetailsDto>
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
}