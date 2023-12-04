using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Image")]
public partial class Image : BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    public string? Medium { get; set; }

    public string? Original { get; set; }

    [InverseProperty("Image")]
    public virtual IList<ShowImage> ShowImages { get; set; } = new List<ShowImage>();
}
