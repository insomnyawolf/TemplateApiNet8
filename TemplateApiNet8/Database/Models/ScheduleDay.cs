using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ScheduleDay")]
public partial class ScheduleDay : BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowScheduleId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid DayId { get; set; }

    [ForeignKey("DayId")]
    [InverseProperty("ScheduleDays")]
    public virtual Day Day { get; set; } = null!;

    [ForeignKey("ShowScheduleId")]
    [InverseProperty("ScheduleDays")]
    public virtual ShowSchedule ShowSchedule { get; set; } = null!;
}
