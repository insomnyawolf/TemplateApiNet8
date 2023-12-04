using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowKind")]
public partial class ShowKind : BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid KindId { get; set; }

    [ForeignKey("KindId")]
    [InverseProperty("ShowKinds")]
    public virtual Kind Kind { get; set; } = null!;

    [ForeignKey("ShowId")]
    [InverseProperty("ShowKinds")]
    public virtual Show Show { get; set; } = null!;
}
