using FluentValidation;

namespace IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;

// Fluent validation for GetNoteDetailsQuery
public class GetNoteDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
{
    public GetNoteDetailsQueryValidator()
    {
        RuleFor(note => note.Id).NotEqual(Guid.Empty);
        RuleFor(note => note.UserId).NotEqual(Guid.Empty);
    }
}