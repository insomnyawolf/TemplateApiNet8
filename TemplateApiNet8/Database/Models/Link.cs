using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Index("Id", IsUnique = true)]
public partial class Link
{
    [Key]
    public int Id { get; set; }

    public int ShowId { get; set; }

    public int? SelfId { get; set; }

    public int? PreviousId { get; set; }

    [ForeignKey("PreviousId")]
    [InverseProperty("LinkPrevious")]
    public virtual Episode? Previous { get; set; }

    [ForeignKey("SelfId")]
    [InverseProperty("LinkSelves")]
    public virtual Episode? Self { get; set; }
}
