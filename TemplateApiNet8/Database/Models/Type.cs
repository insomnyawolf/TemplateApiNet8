using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Type")]
[Index("Id", IsUnique = true)]
public partial class Type
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    [InverseProperty("Type")]
    public virtual ICollection<ShowType> ShowTypes { get; set; } = new List<ShowType>();
}
