using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Day")]
[Index("Id", IsUnique = true)]
public partial class Day
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [InverseProperty("Day")]
    public virtual ICollection<ScheduleDay> ScheduleDays { get; set; } = new List<ScheduleDay>();
}
