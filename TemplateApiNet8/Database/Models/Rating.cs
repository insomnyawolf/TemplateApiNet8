using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Rating")]
[Index("Id", IsUnique = true)]
public partial class Rating
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public double Average { get; set; }

    [ForeignKey("ShowId")]
    [InverseProperty("Ratings")]
    public virtual Show Show { get; set; } = null!;
}
