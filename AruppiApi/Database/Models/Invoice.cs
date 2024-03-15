using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AruppiApi.Database.Models;

[Table("Invoice")]
[Index("CustomerId", Name = "IFK_InvoiceCustomerId")]
public partial class Invoice
{
    [Key]
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime InvoiceDate { get; set; }

    [Column(TypeName = "NVARCHAR(70)")]
    public string? BillingAddress { get; set; }

    [Column(TypeName = "NVARCHAR(40)")]
    public string? BillingCity { get; set; }

    [Column(TypeName = "NVARCHAR(40)")]
    public string? BillingState { get; set; }

    [Column(TypeName = "NVARCHAR(40)")]
    public string? BillingCountry { get; set; }

    [Column(TypeName = "NVARCHAR(10)")]
    public string? BillingPostalCode { get; set; }

    [Column(TypeName = "NUMERIC(10,2)")]
    public double Total { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Invoices")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Invoice")]
    public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();
}
