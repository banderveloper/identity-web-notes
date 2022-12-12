using IdentityWebNotes.Application.Common.Exceptions;
using IdentityWebNotes.Application.Notes.Commands.UpdateNote;
using IdentityWebNotes.Domain;
using IdentityWebNotes.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebNotes.Tests.Notes.Commands;

public class UpdateNoteCommandHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateNoteCommandHandler_Success()
    {
        // Arrange
        var handler = new UpdateNoteCommandHandler(Context);
        var updatedTitle = "updated title";

        // Act
        await handler.Handle(new UpdateNoteCommand
        {
            Id = NotesContextFactory.NoteIdForUpdate,
            UserId = NotesContextFactory.UserBId,
            Title = updatedTitle
        }, CancellationToken.None);

        // Assert
        Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
            note.Id == NotesContextFactory.NoteIdForUpdate && note.Title == updatedTitle));
    }

    [Fact]
    public async Task UpdateNoteCommandHandler_FailOnWrongId()
    {
        // Arrange
        var handler = new UpdateNoteCommandHandler(Context);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new UpdateNoteCommand
            {
                Id = new Guid(),
                UserId = NotesContextFactory.UserAId
            }, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateNoteCommandHandler_FailOnWrongUserId()
    {
        // Arrange
        var handler = new UpdateNoteCommandHandler(Context);

        // Act
        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new UpdateNoteCommand
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                UserId = NotesContextFactory.UserAId
            }, CancellationToken.None));
    }
}