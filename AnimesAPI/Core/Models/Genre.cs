using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Models;

[Index("Name", Name = "UQ__Genres__737584F620D296DD", IsUnique = true)]
public partial class Genre
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [ForeignKey("GenreId")]
    [InverseProperty("Genres")]
    public virtual ICollection<Anime> Animes { get; set; } = new List<Anime>();
}
