using AutoMapper;
using AutoMapper.QueryableExtensions;
using IdentityWebNotes.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityWebNotes.Application.Notes.Queries.GetNoteList;

// cqrs query for getting all notes lookups (id and title) with given user id
public class GetNoteListQueryHandler : IRequestHandler<GetNoteListQuery, NoteListModel>
{
    private readonly INotesDbContext _context;
    private readonly IMapper _mapper;

    public GetNoteListQueryHandler(INotesDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // Accepts user id, returns all notes lookups(id+title) from user with given id 
    public async Task<NoteListModel> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
    {
        // get all notes with given user id
        // .ProjectTo - automapper extension, here maps List<Note> to List<NoteLookupDto>
        var noteLookupDtos = await _context.Notes.Where(
                note => note.UserId == request.UserId)
            .ProjectTo<NoteLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new NoteListModel { Notes = noteLookupDtos };
    }
}