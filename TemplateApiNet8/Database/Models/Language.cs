using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Language")]
public partial class Language
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    [InverseProperty("Language")]
    public virtual ICollection<ShowLanguage> ShowLanguages { get; set; } = new List<ShowLanguage>();
}
