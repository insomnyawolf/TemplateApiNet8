using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Network")]
public partial class Network
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? OfficialSite { get; set; }

    [InverseProperty("Network")]
    public virtual ICollection<CountryNetwork> CountryNetworks { get; set; } = new List<CountryNetwork>();

    [InverseProperty("Network")]
    public virtual ICollection<ShowNetwork> ShowNetworks { get; set; } = new List<ShowNetwork>();
}
