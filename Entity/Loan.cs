using System;
using System.Collections.Generic;

namespace dotnet_web_mvc.Entity;

public partial class Loan
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public int Amount { get; set; }

    public int Interest { get; set; }

    public int NoOfPayment { get; set; }

    public int Deduction { get; set; }

    public string Status { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public int Penalty { get; set; }

    public int Receivable { get; set; }

    public DateTime DateCreated { get; set; }
}
