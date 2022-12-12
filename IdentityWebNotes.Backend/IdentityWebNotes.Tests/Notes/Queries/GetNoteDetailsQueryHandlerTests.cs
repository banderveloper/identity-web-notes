using AutoMapper;
using IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;
using IdentityWebNotes.Application.Notes.Queries.GetNoteList;
using IdentityWebNotes.Persistence;
using IdentityWebNotes.Tests.Common;
using Shouldly;

namespace IdentityWebNotes.Tests.Notes.Queries;

// what it is - check QueryTestFixture.cs bottom
[Collection("QueryCollection")]
public class GetNoteDetailsQueryHandlerTests
{
    private readonly NotesDbContext Context;
    private readonly IMapper Mapper;

    public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
    {
        Context = fixture.Context;
        Mapper = fixture.Mapper;
    }

    [Fact]
    public async Task GetNoteDetailsQueryHandler_Success()
    {
        // Arrange
        var handler = new GetNoteDetailsQueryHandler(Context, Mapper);
        
        // Act
        var result = await handler.Handle(
            new GetNoteDetailsQuery
            {
                UserId = NotesContextFactory.UserBId,
                Id = Guid.Parse("76E3F3CF-4DAA-47BF-A49F-A753FC456E4F")
            }, CancellationToken.None);
        
        // Assert
        result.ShouldBeOfType<NoteDetailsModel>();
        result.Title.ShouldBe("Title2");
        result.CreationDate.ShouldBe(DateTime.Today);
    }
}