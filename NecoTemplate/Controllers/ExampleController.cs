using MediatR;
using Microsoft.AspNetCore.Mvc;
using NecoTemplate.API.CustomExceptionHandler;
using NecoTemplate.Application.Logic.Examples.CreateExample;
using NecoTemplate.Application.Logic.Examples.DeleteExample;
using NecoTemplate.Application.Logic.Examples.EditExample;
using NecoTemplate.Application.Logic.Examples.GetExample;
using NecoTemplate.Application.Logic.Examples.GetExamples;
using NecoTemplate.Domain.Models.Examples;

namespace NecoTemplate.API.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ExampleController:ControllerBase
{
    private ISender _sender;
    public ExampleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("CreateExample")]
    public async Task<IActionResult> CreateExample([FromBody] CreateExampleCommand command)
    {
        var result = await _sender.Send(command).ConfigureAwait(false);
        return result.IsSuccess ? Ok(result.Value) : CustomResult.ToActionResult(result.Error);
    }

    [HttpPost("EditExample")]
    public async Task<IActionResult> EditExample([FromBody] EditExampleCommand command)
    {
        var result = await _sender.Send(command).ConfigureAwait(false);
        return result.IsSuccess ? Ok(result) : CustomResult.ToActionResult(result.Error);
    }

    [HttpPost("DeleteExample")]
    public async Task<IActionResult> DeleteExample([FromBody] DeleteExampleCommand command)
    {
        var result = await _sender.Send(command).ConfigureAwait(false);
        return result.IsSuccess ? Ok(result) : CustomResult.ToActionResult(result.Error);
    }

    [HttpGet("GetExample/{id}")]
    public async Task<IActionResult> GetExample(Guid id)
    {
        var query = new GetExampleQuery(id);
        var result = await _sender.Send(query).ConfigureAwait(false);
        return result.IsSuccess ? Ok(result) : CustomResult.ToActionResult(result.Error);
    }

    [HttpGet("GetExamples")]
    public async Task<IActionResult> GetExamples()
    {
        var query = new GetExamplesQuery();
        var result = await _sender.Send(query).ConfigureAwait(false);
        return result.IsSuccess ? Ok(result) : CustomResult.ToActionResult(result.Error);
    }
}
