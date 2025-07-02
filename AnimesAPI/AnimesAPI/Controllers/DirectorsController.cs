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
        _logger.LogInformation("Buscando todos os diretores.");

        var query = new GetDirectorsQuery();
        var result = await _mediator.Send(query);

        if (!result.Any())
        {
            _logger.LogInformation("Nenhum diretor encontrado.");
            return Ok(Enumerable.Empty<DirectorDto>());
        }

        _logger.LogInformation("{DirectorCount} diretores encontrados.", result.Count());
        return Ok(result);
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
        _logger.LogInformation("Iniciando a criação de um novo diretor com o nome: {DirectorName}", command.Name);

        var result = await _mediator.Send(command);

        _logger.LogInformation("Diretor {DirectorName} com Id {DirectorId} criado com sucesso.", result.Name, result.Id);

        return CreatedAtAction(nameof(GetDirectors), new { id = result.Id }, result);
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
            _logger.LogWarning("O ID ({RouteId}) da rota não corresponde ao ID ({BodyId}) do corpo da requisição.", id, command.Id);
            return BadRequest("O ID do diretor na rota não corresponde ao ID no corpo da requisição.");
        }

        _logger.LogInformation("Iniciando a atualização do diretor com Id: {DirectorId}", id);

        var result = await _mediator.Send(command);

        _logger.LogInformation("Diretor com Id {DirectorId} atualizado com sucesso.", id);
        return Ok(result);
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
        _logger.LogInformation("Iniciando a exclusão do diretor com Id: {DirectorId}", id);

        var command = new DeleteDirectorCommand { Id = id };
        await _mediator.Send(command);

        _logger.LogInformation("Diretor com Id {DirectorId} excluído com sucesso.", id);

        return NoContent();
    }
}