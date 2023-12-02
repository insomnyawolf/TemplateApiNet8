using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Index("Id", IsUnique = true)]
public partial class External
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
}
