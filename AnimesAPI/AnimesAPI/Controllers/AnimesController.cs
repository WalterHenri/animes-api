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

    /// <summary>
    /// Busca animes com base nos filtros fornecidos.
    /// </summary>
    /// <param name="id">Filtra os animes pelo ID.</param>
    /// <param name="name">Filtra os animes pelo nome (busca parcial).</param>
    /// <param name="director">Filtra os animes pelo nome do diretor (busca parcial).</param>
    /// <returns>Uma lista de animes que correspondem aos critérios de busca.</returns>
    /// <response code="200">Retorna a lista de animes.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AnimeDto>), 200)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAnimes([FromQuery] int? id, [FromQuery] string? name, [FromQuery] string? director)
    {
        _logger.LogInformation("Buscando animes com os filtros: Id={AnimeId}, Name={AnimeName}, Director={DirectorName}", id, name, director);

        var query = new GetAnimesQuery { Id = id, Name = name, Director = director };
        var result = await _mediator.Send(query);

        if (!result.Any())
        {
            _logger.LogInformation("Nenhum anime encontrado para os filtros fornecidos.");
            return Ok(Enumerable.Empty<AnimeDto>());
        }

        _logger.LogInformation("{AnimeCount} animes encontrados.", result.Count());
        return Ok(result);
    }

    /// <summary>
    /// Cria um novo anime.
    /// </summary>
    /// <param name="command">Os dados para a criação do novo anime.</param>
    /// <returns>O anime recém-criado.</returns>
    /// <response code="201">Retorna o anime recém-criado.</response>
    /// <response code="400">Se os dados fornecidos forem inválidos.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAnime([FromBody] CreateAnimeCommand command)
    {
        _logger.LogInformation("Iniciando a criação de um novo anime com o nome: {AnimeName}", command.Name);

        var result = await _mediator.Send(command);

        _logger.LogInformation("Anime {AnimeName} com Id {AnimeId} criado com sucesso.", result.Name, result.Id);

        return CreatedAtAction(nameof(GetAnimes), new { id = result.Id }, result);
    }


    /// <summary>
    /// Atualiza um anime existente.
    /// </summary>
    /// <param name="id">O ID do anime a ser atualizado.</param>
    /// <param name="command">Os novos dados para o anime.</param>
    /// <returns>O anime atualizado.</returns>
    /// <response code="200">Retorna o anime atualizado.</response>
    /// <response code="400">Se o ID da rota não corresponder ao ID do corpo da requisição.</response>
    /// <response code="404">Se o anime não for encontrado.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAnime(int id, [FromBody] UpdateAnimeCommand command)
    {
        if (id != command.Id)
        {
            _logger.LogWarning("O ID ({RouteId}) da rota não corresponde ao ID ({BodyId}) do corpo da requisição.", id, command.Id);
            return BadRequest("O ID do anime na rota não corresponde ao ID no corpo da requisição.");
        }

        _logger.LogInformation("Iniciando a atualização do anime com Id: {AnimeId}", id);

        var result = await _mediator.Send(command);

        _logger.LogInformation("Anime com Id {AnimeId} atualizado com sucesso.", id);
        return Ok(result);
    }


    /// <summary>
    /// Exclui um anime específico.
    /// </summary>
    /// <param name="id">O ID do anime a ser excluído.</param>
    /// <returns>Nenhum conteúdo.</returns>
    /// <response code="204">Se o anime for excluído com sucesso.</response>
    /// <response code="404">Se o anime não for encontrado.</response>
    /// <response code="500">Se ocorrer um erro interno no servidor.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAnime(int id)
    {
        _logger.LogInformation("Iniciando a exclusão do anime com Id: {AnimeId}", id);

        var command = new DeleteAnimeCommand { Id = id };
        await _mediator.Send(command);

        _logger.LogInformation("Anime com Id {AnimeId} excluído com sucesso.", id);
        return NoContent();
    }
}