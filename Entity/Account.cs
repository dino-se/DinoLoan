using System;
using System.Collections.Generic;

namespace DinoLoan.Entity;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}
