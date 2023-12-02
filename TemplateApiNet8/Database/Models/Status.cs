using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Status")]
[Index("Id", IsUnique = true)]
public partial class Status
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<ShowStatus> ShowStatuses { get; set; } = new List<ShowStatus>();
}
