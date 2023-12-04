using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Episode")]
public partial class Episode : BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    public string? Href { get; set; }

    [InverseProperty("Previous")]
    public virtual ICollection<ShowLink> ShowLinkPrevious { get; set; } = new List<ShowLink>();

    [InverseProperty("Self")]
    public virtual ICollection<ShowLink> ShowLinkSelves { get; set; } = new List<ShowLink>();
}
