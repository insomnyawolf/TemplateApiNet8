using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Schedule")]
public partial class Schedule
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    public string? Time { get; set; }

    [InverseProperty("Schedule")]
    public virtual ICollection<ScheduleDay> ScheduleDays { get; set; } = new List<ScheduleDay>();

    [ForeignKey("ShowId")]
    [InverseProperty("Schedules")]
    public virtual Show Show { get; set; } = null!;
}
