using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowStatus")]
[Index("Id", IsUnique = true)]
public partial class ShowStatus
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int StatusId { get; set; }

    [ForeignKey("ShowId")]
    [InverseProperty("ShowStatuses")]
    public virtual Show Show { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("ShowStatuses")]
    public virtual Status Status { get; set; } = null!;
}
