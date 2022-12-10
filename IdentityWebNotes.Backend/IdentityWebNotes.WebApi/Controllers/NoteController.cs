using IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;
using IdentityWebNotes.Application.Notes.Queries.GetNoteList;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWebNotes.WebApi.Controllers;

public class NoteController : BaseController
{
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
}