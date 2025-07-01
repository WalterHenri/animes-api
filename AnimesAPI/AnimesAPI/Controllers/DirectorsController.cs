using Animes.Application.DTOs;
using Animes.Application.Features.Directors.Commands;
using Animes.Application.Features.Directors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/[controller]")]
public class DirectorsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<DirectorsController> _logger;

    public DirectorsController(IMediator mediator, ILogger<DirectorsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Busca diretores com base nos filtros fornecidos.
    /// </summary>
    /// <returns>Uma lista de diretores.</returns>
    /// <response code="200">Retorna a lista de diretores.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DirectorDto>), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetDirectors()
    {
        try
        {
            var query = new GetDirectorsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao buscar os diretores.");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }

    /// <summary>
    /// Cria um novo diretor.
    /// </summary>
    /// <param name="command">Os dados para a criação do novo diretor.</param>
    /// <returns>O diretor recém-criado.</returns>
    /// <response code="201">Retorna o diretor recém-criado.</response>
    /// <response code="400">Se os dados fornecidos forem inválidos.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateDirector([FromBody] CreateDirectorCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetDirectors), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o diretor.");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }

    /// <summary>
    /// Atualiza um diretor existente.
    /// </summary>
    /// <param name="id">O ID do diretor a ser atualizado.</param>
    /// <param name="command">Os novos dados para o diretor.</param>
    /// <returns>O diretor atualizado.</returns>
    /// <response code="200">Retorna o diretor atualizado.</response>
    /// <response code="400">Se o ID da rota não corresponder ao ID do corpo da requisição.</response>
    /// <response code="404">Se o diretor não for encontrado.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateDirector(int id, [FromBody] UpdateDirectorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("O ID do diretor na rota não corresponde ao ID no corpo da requisição.");
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
            _logger.LogError(ex, $"Ocorreu um erro ao atualizar o diretor com ID {id}.");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }

    /// <summary>
    /// Exclui um diretor específico.
    /// </summary>
    /// <param name="id">O ID do diretor a ser excluído.</param>
    /// <returns>Nenhum conteúdo.</returns>
    /// <response code="204">Se o diretor for excluído com sucesso.</response>
    /// <response code="404">Se o diretor não for encontrado.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteDirector(int id)
    {
        try
        {
            var command = new DeleteDirectorCommand { Id = id };
            var result = await _mediator.Send(command);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ocorreu um erro ao excluir o diretor com ID {id}.");
            return StatusCode(500, "Ocorreu um erro interno no servidor.");
        }
    }
}