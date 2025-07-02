using System.ComponentModel.DataAnnotations;

namespace Animes.Application.DTOs;

/// <summary>
/// Representa os dados de um diretor a serem expostos pela API.
/// </summary>
public class DirectorDto
{
    /// <summary>
    /// O ID único do diretor.
    /// </summary>
    /// <example>1</example>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// O nome do diretor.
    /// </summary>
    /// <example>Hayao Miyazaki</example>
    public required string Name { get; set; }
}