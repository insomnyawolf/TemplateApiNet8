using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Image")]
[Index("Id", IsUnique = true)]
public partial class Image
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public string? Medium { get; set; }

    public string? Original { get; set; }
}
