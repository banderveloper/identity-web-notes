using FluentValidation;

namespace IdentityWebNotes.Application.Notes.Commands.DeleteNote;

// Fluent validation for DeleteNoteCommand 
public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(deleteNoteCommand =>
            deleteNoteCommand.Id).NotEqual(Guid.Empty);
        RuleFor(deleteNoteCommand =>
            deleteNoteCommand.UserId).NotEqual(Guid.Empty);
    }
}