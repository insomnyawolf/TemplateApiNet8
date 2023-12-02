using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Kind")]
[Index("Id", IsUnique = true)]
public partial class Kind
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    [InverseProperty("Kind")]
    public virtual ICollection<ShowKind> ShowKinds { get; set; } = new List<ShowKind>();
}
