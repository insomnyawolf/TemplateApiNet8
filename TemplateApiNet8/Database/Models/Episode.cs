using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Episode")]
public partial class Episode
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    public string? Href { get; set; }

    [InverseProperty("Previous")]
    public virtual ICollection<Link> LinkPrevious { get; set; } = new List<Link>();

    [InverseProperty("Self")]
    public virtual ICollection<Link> LinkSelves { get; set; } = new List<Link>();
}
