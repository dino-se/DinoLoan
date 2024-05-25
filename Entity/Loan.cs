using System;
using System.Collections.Generic;

namespace DinoLoan.Entity;

public partial class Loan
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public string Type { get; set; }  = null!;

    public decimal Amount { get; set; }

    public decimal Interest { get; set; }

    public decimal InterestAmount { get; set; }

    public int NoOfPayment { get; set; }

    public decimal Deduction { get; set; }

    public string? Status { get; set; }

    public decimal Receivable { get; set; }

    public decimal TotalPayable { get; set; }

    public decimal Collected { get; set; }

    public decimal Collectable { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DueDate { get; set; }
}
