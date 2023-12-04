using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Kind")]
public partial class Kind : BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    public string? Name { get; set; }

    [InverseProperty("Kind")]
    public virtual IList<ShowKind> ShowKinds { get; set; } = new List<ShowKind>();
}
