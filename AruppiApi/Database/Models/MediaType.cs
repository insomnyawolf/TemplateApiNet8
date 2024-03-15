using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AruppiApi.Database.Models;

[Table("MediaType")]
public partial class MediaType
{
    [Key]
    public int MediaTypeId { get; set; }

    [Column(TypeName = "NVARCHAR(120)")]
    public string? Name { get; set; }

    [InverseProperty("MediaType")]
    public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
}
