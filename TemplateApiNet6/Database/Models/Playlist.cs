using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using TemplateApiNet6.Database.Infraestructure;

namespace TemplateApiNet6.Database.Models;

[Table("Playlist")]
public partial class Playlist
{
    [Key]
    public long PlaylistId { get; set; }

    [Column(TypeName = "NVARCHAR(120)")]
    public string? Name { get; set; }

    [ForeignKey("PlaylistId")]
    [InverseProperty("Playlists")]
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();

    // : ISoftDeleted, IMultiTenant
    // public bool IsDeleted { get; set; }
    // public int TenantId { get; set; }
}
