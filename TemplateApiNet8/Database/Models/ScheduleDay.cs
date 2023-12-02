using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ScheduleDay")]
[Index("Id", IsUnique = true)]
public partial class ScheduleDay
{
    [Key]
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public int DayId { get; set; }

    [ForeignKey("DayId")]
    [InverseProperty("ScheduleDays")]
    public virtual Day Day { get; set; } = null!;

    [ForeignKey("ScheduleId")]
    [InverseProperty("ScheduleDays")]
    public virtual Schedule Schedule { get; set; } = null!;
}
