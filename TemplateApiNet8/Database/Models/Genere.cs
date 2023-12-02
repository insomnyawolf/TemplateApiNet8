using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Genere")]
[Index("Id", IsUnique = true)]
public partial class Genere
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [InverseProperty("Genere")]
    public virtual ICollection<ShowGenere> ShowGeneres { get; set; } = new List<ShowGenere>();
}
