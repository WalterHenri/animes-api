using Animes.Application.DTOs;
using Animes.Application.Features.Animes.Commands;
using Animes.Application.Features.Animes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class AnimesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AnimesController> _logger;

    public AnimesController(IMediator mediator, ILogger<AnimesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnimes([FromQuery] int? id, [FromQuery] string? name, [FromQuery] string? director)
    {
        try
        {
            var query = new GetAnimesQuery { Id = id, Name = name, Director = director };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar os animes.");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAnime([FromBody] CreateAnimeCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAnimes), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o anime.");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAnime(int id, [FromBody] UpdateAnimeCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("O ID do anime na rota não corresponde ao ID no corpo da requisição.");
        }

        try
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao atualizar o anime com ID {id}.");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAnime(int id)
    {
        try
        {
            var command = new DeleteAnimeCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao excluir o anime com ID {id}.");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }
}