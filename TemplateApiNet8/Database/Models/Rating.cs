using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Rating")]
public partial class Rating
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    public double Average { get; set; }

    [ForeignKey("ShowId")]
    [InverseProperty("Ratings")]
    public virtual Show Show { get; set; } = null!;
}
