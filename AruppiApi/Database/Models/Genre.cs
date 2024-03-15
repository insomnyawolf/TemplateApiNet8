using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AruppiApi.Database.Models;

[Table("Genre")]
public partial class Genre
{
    [Key]
    public int GenreId { get; set; }

    [Column(TypeName = "NVARCHAR(120)")]
    public string? Name { get; set; }

    [InverseProperty("Genre")]
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
