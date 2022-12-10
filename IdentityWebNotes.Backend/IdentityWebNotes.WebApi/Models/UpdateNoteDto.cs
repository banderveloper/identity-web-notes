using AutoMapper;
using IdentityWebNotes.Application.Common.Mappings;
using IdentityWebNotes.Application.Notes.Commands.UpdateNote;

namespace IdentityWebNotes.WebApi.Models;

public class UpdateNoteDto : IMapWith<UpdateNoteCommand>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
            .ForMember(noteCommand => noteCommand.Id,
                options =>
                    options.MapFrom(noteDto => noteDto.Id))
            .ForMember(noteCommand => noteCommand.Title,
                options =>
                    options.MapFrom(noteDto => noteDto.Title))
            .ForMember(noteCommand => noteCommand.Details,
                options =>
                    options.MapFrom(noteDto => noteDto.Details));
    }
}