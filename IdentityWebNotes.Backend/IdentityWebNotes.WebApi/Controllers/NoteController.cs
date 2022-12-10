using AutoMapper;
using IdentityWebNotes.Application.Notes.Commands.CreateNote;
using IdentityWebNotes.Application.Notes.Commands.DeleteNote;
using IdentityWebNotes.Application.Notes.Commands.UpdateNote;
using IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;
using IdentityWebNotes.Application.Notes.Queries.GetNoteList;
using IdentityWebNotes.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWebNotes.WebApi.Controllers;

[Route("api/[controller]")]
public class NoteController : BaseController
{
    private IMapper _mapper;

    public NoteController(IMapper mapper)
        => _mapper = mapper;


    [HttpGet]
    public async Task<ActionResult<NoteListModel>> GetAll()
    {
        // cqrs query that accepts user id
        var query = new GetNoteListQuery { UserId = base.UserId };

        // send this query to handler via mediator, get NodeListModel and send to client
        NoteListModel model = await Mediator.Send(query);
        return Ok(model);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NoteDetailsModel>> GetById(Guid id)
    {
        var query = new GetNoteDetailsQuery
        {
            Id = id,
            UserId = base.UserId
        };

        NoteDetailsModel model = await Mediator.Send(query);
        return Ok(model);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = base.UserId;

        Guid noteId = await Mediator.Send(command);

        return Ok(noteId);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.UserId = base.UserId;
        
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteNoteCommand()
        {
            Id = id,
            UserId = base.UserId
        };

        await Mediator.Send(command);
        return NoContent();
    }
}