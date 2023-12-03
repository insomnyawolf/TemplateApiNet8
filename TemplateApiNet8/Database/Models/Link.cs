using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

public partial class Link
{
    [Key]
    [Column(TypeName = "GUID")]
    public Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid? SelfId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid? PreviousId { get; set; }

    [ForeignKey("PreviousId")]
    [InverseProperty("LinkPrevious")]
    public virtual Episode? Previous { get; set; }

    [ForeignKey("SelfId")]
    [InverseProperty("LinkSelves")]
    public virtual Episode? Self { get; set; }
}
