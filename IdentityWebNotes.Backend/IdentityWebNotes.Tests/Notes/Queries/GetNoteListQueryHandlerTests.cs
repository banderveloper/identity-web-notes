using AutoMapper;
using IdentityWebNotes.Application.Notes.Queries.GetNoteList;
using IdentityWebNotes.Persistence;
using IdentityWebNotes.Tests.Common;
using Shouldly;

namespace IdentityWebNotes.Tests.Notes.Queries;

// what it is - check QueryTestFixture.cs bottom
[Collection("QueryCollection")]
public class GetNoteListQueryHandlerTests
{
    private readonly NotesDbContext Context;
    private readonly IMapper Mapper;

    public GetNoteListQueryHandlerTests(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetNoteListQueryHandler_Success()
    {
        // Arrange
        var handler = new GetNoteListQueryHandler(Context, Mapper);

        // Act
        var result = await handler.Handle(
            new GetNoteListQuery
            {
                UserId = NotesContextFactory.UserBId
            }, CancellationToken.None);

        // Assert
        result.ShouldBeOfType<NoteListModel>();
        result.Notes.Count.ShouldBe(2);
    }
}