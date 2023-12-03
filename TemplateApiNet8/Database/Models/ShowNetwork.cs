using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowNetwork")]
public partial class ShowNetwork
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid NetworkId { get; set; }

    [ForeignKey("NetworkId")]
    [InverseProperty("ShowNetworks")]
    public virtual Network Network { get; set; } = null!;

    [ForeignKey("ShowId")]
    [InverseProperty("ShowNetworks")]
    public virtual Show Show { get; set; } = null!;
}
