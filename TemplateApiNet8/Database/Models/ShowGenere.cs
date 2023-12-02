using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowGenere")]
[Index("Id", IsUnique = true)]
public partial class ShowGenere
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int GenereId { get; set; }

    [ForeignKey("GenereId")]
    [InverseProperty("ShowGeneres")]
    public virtual Genere Genere { get; set; } = null!;

    [ForeignKey("ShowId")]
    [InverseProperty("ShowGeneres")]
    public virtual Show Show { get; set; } = null!;
}
