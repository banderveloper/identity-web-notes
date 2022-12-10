using AutoMapper;
using IdentityWebNotes.Application.Common.Mappings;
using IdentityWebNotes.Domain;

namespace IdentityWebNotes.Application.Notes.Queries.GetNoteList;

// Dto, that sends to client, used for GetNoteListQueryHandler
public class NoteLookupDto : IMapWith<Note>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Note, NoteLookupDto>()
            .ForMember(noteDto => noteDto.Id,
                options =>
                    options.MapFrom(note => note.Id))
            .ForMember(noteDto => noteDto.Title,
                options =>
                    options.MapFrom(note => note.Title));
    }
}