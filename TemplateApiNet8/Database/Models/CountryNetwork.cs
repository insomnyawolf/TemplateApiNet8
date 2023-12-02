using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("CountryNetwork")]
[Index("Id", IsUnique = true)]
public partial class CountryNetwork
{
    [Key]
    public int Id { get; set; }

    public int NetworkId { get; set; }

    public int CountryId { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("CountryNetworks")]
    public virtual Country Country { get; set; } = null!;

    [ForeignKey("NetworkId")]
    [InverseProperty("CountryNetworks")]
    public virtual Network Network { get; set; } = null!;
}
