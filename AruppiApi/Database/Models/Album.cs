using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AruppiApi.Database.Models;

[Table("Album")]
[Index("ArtistId", Name = "IFK_AlbumArtistId")]
public partial class Album
{
    [Key]
    public int AlbumId { get; set; }

    [Column(TypeName = "NVARCHAR(160)")]
    public string Title { get; set; } = null!;

    public int ArtistId { get; set; }

    [ForeignKey("ArtistId")]
    [InverseProperty("Albums")]
    public virtual Artist Artist { get; set; } = null!;

    [InverseProperty("Album")]
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
