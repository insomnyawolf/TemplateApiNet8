using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("CountryNetwork")]
public partial class CountryNetwork: BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid NetworkId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid CountryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("CountryNetworks")]
    public virtual Country Country { get; set; } = null!;

    [ForeignKey("NetworkId")]
    [InverseProperty("CountryNetworks")]
    public virtual Network Network { get; set; } = null!;
}
