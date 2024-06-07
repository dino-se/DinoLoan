using System.ComponentModel.DataAnnotations;

namespace DinoLoan.ViewModels
{
    public partial class PaymentViewModel
    {
        public int PaymentId { get; set; }

        public int LoanId { get; set; }

        public int ClientId { get; set; }

        public decimal Collectable { get; set; }

        public decimal CollectableOG { get; set; }

        public DateTime Date { get; set; }
    }
}