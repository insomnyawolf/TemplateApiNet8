using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("External")]
public partial class External
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    [InverseProperty("External")]
    public virtual ICollection<ShowExternal> ShowExternals { get; set; } = new List<ShowExternal>();
}
