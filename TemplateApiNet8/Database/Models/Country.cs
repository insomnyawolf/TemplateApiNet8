using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Country")]
[Index("Id", IsUnique = true)]
public partial class Country
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public string? Timezone { get; set; }

    [InverseProperty("Country")]
    public virtual ICollection<CountryNetwork> CountryNetworks { get; set; } = new List<CountryNetwork>();
}
