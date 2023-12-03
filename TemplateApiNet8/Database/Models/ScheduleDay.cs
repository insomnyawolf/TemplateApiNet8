using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ScheduleDay")]
public partial class ScheduleDay
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ScheduleId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid DayId { get; set; }

    [ForeignKey("DayId")]
    [InverseProperty("ScheduleDays")]
    public virtual Day Day { get; set; } = null!;

    [ForeignKey("ScheduleId")]
    [InverseProperty("ScheduleDays")]
    public virtual Schedule Schedule { get; set; } = null!;
}
