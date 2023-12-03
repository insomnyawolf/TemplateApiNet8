﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("Show")]
public partial class Show
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

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
    public virtual ICollection<ShowExternal> ShowExternals { get; set; } = new List<ShowExternal>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowGenere> ShowGeneres { get; set; } = new List<ShowGenere>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowImage> ShowImages { get; set; } = new List<ShowImage>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowKind> ShowKinds { get; set; } = new List<ShowKind>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowLanguage> ShowLanguages { get; set; } = new List<ShowLanguage>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowNetwork> ShowNetworks { get; set; } = new List<ShowNetwork>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowRating> ShowRatings { get; set; } = new List<ShowRating>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowSchedule> ShowSchedules { get; set; } = new List<ShowSchedule>();

    [InverseProperty("Show")]
    public virtual ICollection<ShowStatus> ShowStatuses { get; set; } = new List<ShowStatus>();
}
