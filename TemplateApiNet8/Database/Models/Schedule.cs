using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Schedule")]
[Index("Id", IsUnique = true)]
public partial class Schedule
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public string? Time { get; set; }

    [InverseProperty("Schedule")]
    public virtual ICollection<ScheduleDay> ScheduleDays { get; set; } = new List<ScheduleDay>();

    [ForeignKey("ShowId")]
    [InverseProperty("Schedules")]
    public virtual Show Show { get; set; } = null!;
}
