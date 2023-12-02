using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Show")]
[Index("Id", IsUnique = true)]
public partial class Show
{
    [Key]
    public int Id { get; set; }

    public string? Url { get; set; }

    public string? Name { get; set; }

    public int? Runtime { get; set; }

    public int? AverageRuntime { get; set; }

    public string? Premiered { get; set; }

    public string? Ended { get; set; }

    public string? OfficialSite { get; set; }

    public int? Weight { get; set; }

    public string? Summary { get; set; }

    public int? Updated { get; set; }

    [InverseProperty("Show")]
    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    [InverseProperty("Show")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowGenere> ShowGeneres { get; set; } = new List<ShowGenere>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowLanguage> ShowLanguages { get; set; } = new List<ShowLanguage>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowNetwork> ShowNetworks { get; set; } = new List<ShowNetwork>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowStatus> ShowStatuses { get; set; } = new List<ShowStatus>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowType> ShowTypes { get; set; } = new List<ShowType>();
}
