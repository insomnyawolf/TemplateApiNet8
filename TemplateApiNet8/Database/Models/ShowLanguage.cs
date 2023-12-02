using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowLanguage")]
[Index("Id", IsUnique = true)]
public partial class ShowLanguage
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int LanguageId { get; set; }

    [ForeignKey("LanguageId")]
    [InverseProperty("ShowLanguages")]
    public virtual Language Language { get; set; } = null!;

    [ForeignKey("ShowId")]
    [InverseProperty("ShowLanguages")]
    public virtual Show Show { get; set; } = null!;
}
