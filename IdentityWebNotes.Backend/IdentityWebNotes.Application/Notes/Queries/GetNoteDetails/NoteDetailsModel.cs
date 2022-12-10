using AutoMapper;
using IdentityWebNotes.Application.Common.Mappings;
using IdentityWebNotes.Domain;

namespace IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;

// Dto, that sends to client, userId is removed because user shouldn't know his id
public class NoteDetailsModel : IMapWith<Note>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public DateTime CreationDate { get; set; }
    public DateTime? EditDate { get; set; }

    // Custom mapping NOTE to NOTEDETAILSMODEL , that overlaps default realisation from IMapWith
    // Some unnecessary method, because EXACTLY THIS mapping can work using default implementations
    // So, i leave it here for example :)
    void Mapping(Profile profile)
    {
        // direct mapping without any changes, can be replaced with default implementation, but let it be here for demonstration
        profile.CreateMap<Note, NoteDetailsModel>()
            .ForMember(noteDto => noteDto.Title,
                options => options.MapFrom(note => note.Title))
            .ForMember(noteDto => noteDto.Details,
                options => options.MapFrom(note => note.Details))
            .ForMember(noteDto => noteDto.Id,
                options => options.MapFrom(note => note.Id))
            .ForMember(noteDto => noteDto.CreationDate,
                options => options.MapFrom(note => note.CreationDate))
            .ForMember(noteDto => noteDto.EditDate,
                options => options.MapFrom(note => note.EditDate));
    }
}