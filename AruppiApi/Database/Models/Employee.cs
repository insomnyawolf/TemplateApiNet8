using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AruppiApi.Database.Models;

[Table("Employee")]
[Index("ReportsTo", Name = "IFK_EmployeeReportsTo")]
public partial class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [Column(TypeName = "NVARCHAR(20)")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "NVARCHAR(20)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "NVARCHAR(30)")]
    public string? Title { get; set; }

    public int? ReportsTo { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? BirthDate { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? HireDate { get; set; }

    [Column(TypeName = "NVARCHAR(70)")]
    public string? Address { get; set; }

    [Column(TypeName = "NVARCHAR(40)")]
    public string? City { get; set; }

    [Column(TypeName = "NVARCHAR(40)")]
    public string? State { get; set; }

    [Column(TypeName = "NVARCHAR(40)")]
    public string? Country { get; set; }

    [Column(TypeName = "NVARCHAR(10)")]
    public string? PostalCode { get; set; }

    [Column(TypeName = "NVARCHAR(24)")]
    public string? Phone { get; set; }

    [Column(TypeName = "NVARCHAR(24)")]
    public string? Fax { get; set; }

    [Column(TypeName = "NVARCHAR(60)")]
    public string? Email { get; set; }

    [InverseProperty("SupportRep")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("ReportsToNavigation")]
    public virtual ICollection<Employee> InverseReportsToNavigation { get; set; } = new List<Employee>();

    [ForeignKey("ReportsTo")]
    [InverseProperty("InverseReportsToNavigation")]
    public virtual Employee? ReportsToNavigation { get; set; }
}
