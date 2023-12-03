using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowExternal")]
public partial class ShowExternal: BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ExternalId { get; set; }

    public double? Value { get; set; }

    [ForeignKey("ExternalId")]
    [InverseProperty("ShowExternals")]
    public virtual External External { get; set; } = null!;

    [ForeignKey("ShowId")]
    [InverseProperty("ShowExternals")]
    public virtual Show Show { get; set; } = null!;
}
