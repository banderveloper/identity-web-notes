using MediatR;

namespace IdentityWebNotes.Application.Notes.Queries.GetNoteList;

// cqrs query for GetNoteListQueryHandler
public class GetNoteListQuery : IRequest<NoteListModel>
{
    public Guid UserId { get; set; }
}