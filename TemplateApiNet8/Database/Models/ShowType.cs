using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowType")]
[Index("Id", IsUnique = true)]
public partial class ShowType
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int TypeId { get; set; }

    [ForeignKey("ShowId")]
    [InverseProperty("ShowTypes")]
    public virtual Show Show { get; set; } = null!;

    [ForeignKey("TypeId")]
    [InverseProperty("ShowTypes")]
    public virtual Type Type { get; set; } = null!;
}
