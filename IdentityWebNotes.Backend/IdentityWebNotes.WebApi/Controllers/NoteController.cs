using AutoMapper;
using IdentityWebNotes.Application.Notes.Commands.CreateNote;
using IdentityWebNotes.Application.Notes.Commands.DeleteNote;
using IdentityWebNotes.Application.Notes.Commands.UpdateNote;
using IdentityWebNotes.Application.Notes.Queries.GetNoteDetails;
using IdentityWebNotes.Application.Notes.Queries.GetNoteList;
using IdentityWebNotes.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityWebNotes.WebApi.Controllers;

[Produces("application/json")] // for xml docs
[Authorize]
[Route("api/[controller]")]
public class NoteController : BaseController
{
    private IMapper _mapper;

    public NoteController(IMapper mapper)
        => _mapper = mapper;


    /// <summary>
    /// Get the list of notes
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note 
    /// </remarks>
    /// <returns>Returns NoteListModel</returns>
    /// <response code="200">Success</response>
    /// <response code="404">If the user is unauthorized</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<NoteListModel>> GetAll()
    {
        // cqrs query that accepts user id
        var query = new GetNoteListQuery { UserId = base.UserId };

        // send this query to handler via mediator, get NodeListModel and send to client
        NoteListModel model = await Mediator.Send(query);
        return Ok(model);
    }

    /// <summary>
    /// Gets the note by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note/D34D349E-43B8-429E-BCA4-793C932FD580
    /// </remarks>
    /// <param name="id">Note id (guid)</param>
    /// <returns>Returns NoteDetailsModel</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user in unauthorized</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Creates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /note
    /// {
    ///     title: "note title",
    ///     details: "note details"
    /// }
    /// </remarks>
    /// <param name="createNoteDto">CreateNoteDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
    {
        var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
        command.UserId = base.UserId;

        Guid noteId = await Mediator.Send(command);

        return Ok(noteId);
    }

    /// <summary>
    /// Updates the note
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// PUT /note
    /// {
    ///     title: "updated note title"
    /// }
    /// </remarks>
    /// <param name="updateNoteDto">UpdateNoteDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
    {
        var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
        command.UserId = base.UserId;

        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Deletes the note by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// DELETE /note/88DEB432-062F-43DE-8DCD-8B6EF79073D3
    /// </remarks>
    /// <param name="id">Id of the note (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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