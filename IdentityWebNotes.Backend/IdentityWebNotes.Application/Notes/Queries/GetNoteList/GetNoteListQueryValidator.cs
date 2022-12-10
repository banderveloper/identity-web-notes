using FluentValidation;
using IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;

namespace IdentityWebNotes.Application.Notes.Queries.GetNoteList;

// Fluent validation for GetNoteListQuery
public class GetNoteListQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
    public GetNoteListQueryValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
    }
}