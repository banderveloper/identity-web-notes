using AutoMapper;
using IdentityWebNotes.Application.Common.Mappings;
using IdentityWebNotes.Application.Interfaces;
using IdentityWebNotes.Persistence;

namespace IdentityWebNotes.Tests.Common;

// Class for "injecting" into tests classes constructors
public class QueryTestFixture : IDisposable
{
    public NotesDbContext Context;
    public IMapper Mapper;

    public QueryTestFixture()
    {
        Context = NotesContextFactory.Create();
        var configurationProvider = new MapperConfiguration(config =>
        {
            config.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
        });
        Mapper = configurationProvider.CreateMapper();
    }

    public void Dispose()
    {
        NotesContextFactory.Destroy(Context);
    }

    // Smth like DI in the tests
    // It will pass QueryTestFixture to all tests constructors
    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture>
    {
    }
}