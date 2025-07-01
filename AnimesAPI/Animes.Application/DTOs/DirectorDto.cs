using System.ComponentModel.DataAnnotations;

namespace Animes.Application.DTOs;

public class DirectorDto
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
}