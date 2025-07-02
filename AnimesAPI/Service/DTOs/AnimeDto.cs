using System.ComponentModel.DataAnnotations;

namespace Animes.Application.DTOs;

/// <summary>
/// Representa os dados de um anime a serem expostos pela API.
/// </summary>
public class AnimeDto
{
    /// <summary>
    /// O ID �nico do anime.
    /// </summary>
    /// <example>1</example>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// O nome do anime.
    /// </summary>
    /// <example>A Viagem de Chihiro</example>
    public required string Name { get; set; }

    /// <summary>
    /// Um resumo da hist�ria do anime.
    /// </summary>
    /// <example>Chihiro e seus pais est�o se mudando para uma cidade diferente. No caminho, eles se deparam com um t�nel que os leva a um mundo m�gico e perigoso.</example>
    public required string Summary { get; set; }

    /// <summary>
    /// O ID do diretor do anime.
    /// </summary>
    /// <example>1</example>
    public required int DirectorId { get; set; }

    /// <summary>
    /// O nome do diretor do anime.
    /// </summary>
    /// <example>Hayao Miyazaki</example>
    public required string DirectorName { get; set; }
}