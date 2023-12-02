using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowKind")]
[Index("Id", IsUnique = true)]
public partial class ShowKind
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int KindId { get; set; }

    [ForeignKey("KindId")]
    [InverseProperty("ShowKinds")]
    public virtual Kind Kind { get; set; } = null!;

    [ForeignKey("ShowId")]
    [InverseProperty("ShowKinds")]
    public virtual Show Show { get; set; } = null!;
}
