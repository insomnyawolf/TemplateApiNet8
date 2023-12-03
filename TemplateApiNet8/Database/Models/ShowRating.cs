using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowRating")]
public partial class ShowRating
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    public double Average { get; set; }

    [ForeignKey("ShowId")]
    [InverseProperty("ShowRatings")]
    public virtual Show Show { get; set; } = null!;
}
