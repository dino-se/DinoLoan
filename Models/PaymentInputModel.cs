namespace DinoLoan.Models;

public class PaymentInputModel
{
    public int Lid { get; set; }

    public int Pid { get; set; }

    public decimal Amnt { get; set; }

    public decimal Over { get; set; }

}