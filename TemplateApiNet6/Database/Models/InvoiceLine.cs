using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TemplateApiNet6.Database.Models;

[Table("InvoiceLine")]
[Index("InvoiceId", Name = "IFK_InvoiceLineInvoiceId")]
[Index("TrackId", Name = "IFK_InvoiceLineTrackId")]
public partial class InvoiceLine
{
    [Key]
    public long InvoiceLineId { get; set; }

    public long InvoiceId { get; set; }

    public long TrackId { get; set; }

    [Column(TypeName = "NUMERIC(10,2)")]
    public byte[] UnitPrice { get; set; } = null!;

    public long Quantity { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceLines")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("TrackId")]
    [InverseProperty("InvoiceLines")]
    public virtual Track Track { get; set; } = null!;
}
