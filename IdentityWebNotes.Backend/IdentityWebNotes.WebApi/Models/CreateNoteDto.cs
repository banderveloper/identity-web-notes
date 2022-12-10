using AutoMapper;
using IdentityWebNotes.Application.Common.Mappings;
using IdentityWebNotes.Application.Notes.Commands.CreateNote;

namespace IdentityWebNotes.WebApi.Models;

public class CreateNoteDto : IMapWith<CreateNoteCommand>
{
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateNoteDto, CreateNoteCommand>()
            .ForMember(noteCommand => noteCommand.Title,
                options =>
                    options.MapFrom(noteDto => noteDto.Title))
            .ForMember(noteCommand => noteCommand.Details,
                options =>
                    options.MapFrom(noteDto => noteDto.Details));
    }
}