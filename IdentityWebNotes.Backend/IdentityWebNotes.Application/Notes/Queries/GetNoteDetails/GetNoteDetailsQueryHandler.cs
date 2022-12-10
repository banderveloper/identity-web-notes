using AutoMapper;
using IdentityWebNotes.Application.Common.Exceptions;
using IdentityWebNotes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;

// CQRS query for getting note details, accepting note id and user id
public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsDto>
{
    private readonly INotesDbContext _context;
    private readonly IMapper _mapper;

    public GetNoteDetailsQueryHandler(INotesDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Returns note details without user id. Accepts user id and note id.
    // UserId is removed because client shouldn't know his user id
    public async Task<NoteDetailsDto> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
    {
        // get note entity from db by id
        var noteEntity = await _context.Notes.FirstOrDefaultAsync(
            note => note.Id == request.Id, cancellationToken);

        if (noteEntity is null || noteEntity.UserId != request.UserId)
            throw new NotFoundException(nameof(noteEntity), request.Id);

        // If note is found and user id is equals, map it into NoteDetailsDto and return
        NoteDetailsDto result = _mapper.Map<NoteDetailsDto>(noteEntity);

        return result;
    }
}