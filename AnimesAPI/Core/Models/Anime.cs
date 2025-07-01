using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Models;

public partial class Anime
{
    [Key]
    public int Id { get; set; }

    [StringLength(255)]
    public string Name { get; set; } = null!;

    public string Summary { get; set; } = null!;

    public int? ReleaseYear { get; set; }

    public int? EpisodeCount { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal? Score { get; set; }

    public int? DirectorId { get; set; }

    public int? StudioId { get; set; }

    [ForeignKey("DirectorId")]
    [InverseProperty("Animes")]
    public virtual Director? Director { get; set; }

    [ForeignKey("StudioId")]
    [InverseProperty("Animes")]
    public virtual Studio? Studio { get; set; }

    [ForeignKey("AnimeId")]
    [InverseProperty("Animes")]
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
}
