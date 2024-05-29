using System;
using System.Collections.Generic;

namespace DinoLoan.Entity;

public partial class Transaction
{
    public int Id { get; set; }

    public int LoanId { get; set; }

    public decimal Amount { get; set; }

    public DateTime Date { get; set; }
}
