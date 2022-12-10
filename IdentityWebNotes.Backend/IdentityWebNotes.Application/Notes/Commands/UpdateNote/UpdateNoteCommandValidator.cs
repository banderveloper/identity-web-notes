using FluentValidation;

namespace IdentityWebNotes.Application.Notes.Commands.UpdateNote;

// Fluent Validation for UpdateNoteCommand
public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(updateNoteCommand =>
            updateNoteCommand.UserId).NotEqual(Guid.Empty);
        RuleFor(updateNoteCommand =>
            updateNoteCommand.Id).NotEqual(Guid.Empty);
        RuleFor(updateNoteCommand =>
            updateNoteCommand.Title).MaximumLength(250);
    }
}