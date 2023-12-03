using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Kind")]
public partial class Kind
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    [InverseProperty("Kind")]
    public virtual ICollection<ShowKind> ShowKinds { get; set; } = new List<ShowKind>();
}
