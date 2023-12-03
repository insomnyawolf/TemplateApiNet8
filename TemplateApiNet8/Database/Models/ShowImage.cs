using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet8.Database.Models;

[Table("ShowImage")]
public partial class ShowImage: BaseEntity
{
    [Key]
    [Column(TypeName = "GUID")]
    public override Guid Id { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ShowId { get; set; }

    [Column(TypeName = "GUID")]
    public Guid ImageId { get; set; }

    [ForeignKey("ImageId")]
    [InverseProperty("ShowImages")]
    public virtual Image Image { get; set; } = null!;

    [ForeignKey("ShowId")]
    [InverseProperty("ShowImages")]
    public virtual Show Show { get; set; } = null!;
}
