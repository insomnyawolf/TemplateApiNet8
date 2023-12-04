using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Country")]
public partial class Country : BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Timezone { get; set; }

    [InverseProperty("Country")]
    public virtual IList<CountryNetwork> CountryNetworks { get; set; } = new List<CountryNetwork>();
}
