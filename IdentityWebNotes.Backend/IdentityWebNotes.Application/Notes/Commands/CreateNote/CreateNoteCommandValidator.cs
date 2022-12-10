using FluentValidation;

namespace IdentityWebNotes.Application.Notes.Commands.CreateNote;

// Fluent Validation for CreateNoteCommand
public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
{
    public CreateNoteCommandValidator()
    {
        RuleFor(createNoteCommand =>
            createNoteCommand.Title).NotEmpty().MaximumLength(250);
        RuleFor(createNoteCommand =>
            createNoteCommand.UserId).NotEqual(Guid.Empty);
    }
}