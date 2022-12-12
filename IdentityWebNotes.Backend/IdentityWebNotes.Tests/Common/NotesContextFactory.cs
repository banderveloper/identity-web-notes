using IdentityWebNotes.Domain;
using IdentityWebNotes.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebNotes.Tests.Common;

public class NotesContextFactory
{
    public static Guid UserAId = Guid.NewGuid();
    public static Guid UserBId = Guid.NewGuid();

    public static Guid NoteIdForDelete = Guid.NewGuid();
    public static Guid NoteIdForUpdate = Guid.NewGuid();

    public static NotesDbContext Create()
    {
        var options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new NotesDbContext(options);
        context.Database.EnsureCreated();
        context.Notes.AddRange(
            new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details1",
                EditDate = null,
                Id = Guid.Parse("63F58722-D18B-468C-B775-477BEEF76750"),
                Title = "Title1",
                UserId = UserAId
            },
            new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details2",
                EditDate = null,
                Id = Guid.Parse("76E3F3CF-4DAA-47BF-A49F-A753FC456E4F"),
                Title = "Title2",
                UserId = UserBId
            },
            new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details3",
                EditDate = null,
                Id = Guid.Parse("D871E547-7738-4BD5-B033-16E51E6C2702"),
                Title = "Title3",
                UserId = UserAId
            },
            new Note
            {
                CreationDate = DateTime.Today,
                Details = "Details4",
                EditDate = null,
                Id = Guid.Parse("357B77D6-AD73-41E4-89CD-DBCFFDAA0142"),
                Title = "Title4",
                UserId = UserBId
            }
        );

        context.SaveChanges();
        return context;
    }

    public static void Destroy(NotesDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}