using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

public partial class ShowLink : BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid? SelfId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid? PreviousId { get; set; }

    [ForeignKey("PreviousId")]
    [InverseProperty("ShowLinkPrevious")]
    public virtual Episode? Previous { get; set; }

    [ForeignKey("SelfId")]
    [InverseProperty("ShowLinkSelves")]
    public virtual Episode? Self { get; set; }
}
