using System.ComponentModel.DataAnnotations;

namespace Animes.Application.DTOs;
public class AnimeDto
{
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Summary { get; set; }

    public required int DirectorId { get; set; }
    public required string DirectorName { get; set; }
}