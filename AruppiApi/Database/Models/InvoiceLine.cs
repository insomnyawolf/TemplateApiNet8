using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AruppiApi.Database.Models;

[Table("InvoiceLine")]
[Index("InvoiceId", Name = "IFK_InvoiceLineInvoiceId")]
[Index("TrackId", Name = "IFK_InvoiceLineTrackId")]
public partial class InvoiceLine
{
    [Key]
    public int InvoiceLineId { get; set; }

    public int InvoiceId { get; set; }

    public int TrackId { get; set; }

    [Column(TypeName = "NUMERIC(10,2)")]
    public double UnitPrice { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("InvoiceId")]
    [InverseProperty("InvoiceLines")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("TrackId")]
    [InverseProperty("InvoiceLines")]
    public virtual Track Track { get; set; } = null!;
}
