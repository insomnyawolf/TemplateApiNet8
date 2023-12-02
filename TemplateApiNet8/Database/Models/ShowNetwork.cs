using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowNetwork")]
[Index("Id", IsUnique = true)]
public partial class ShowNetwork
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int NetworkId { get; set; }

    [ForeignKey("NetworkId")]
    [InverseProperty("ShowNetworks")]
    public virtual Network Network { get; set; } = null!;

    [ForeignKey("ShowId")]
    [InverseProperty("ShowNetworks")]
    public virtual Show Show { get; set; } = null!;
}
