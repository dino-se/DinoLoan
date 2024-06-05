using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DinoLoan.Entity;
using DinoLoan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DinoLoan.Controllers
{
    public class PaymentController : Controller
    {
        private readonly DinoloanDbContext _context;

        public PaymentController(DinoloanDbContext payments)
        {
            _context = payments;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            // var payment = _context.Payments.Where(e => e.LoanId == id).ToList();

            var payment = GetPayments(id);

            if (payment == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = payment.FirstOrDefault()?.ClientId;

            return View(payment);
        }

        [HttpPost]
        public IActionResult Index(PaymentViewModel pvm)
        {
            Loan? loan = _context.Loans.FirstOrDefault(l => l.Id == pvm.Lid);
            // Payment? payment = _context.Payments.FirstOrDefault(l => l.PaymentId == pvm.Pid);

            if (loan == null)
            {
                return NotFound();
            }

            loan.Collected += pvm.Amnt;
            loan.Collectable = Math.Max(loan.Collectable - pvm.Amnt, 0);
            //payment.Collectable = Math.Max(payment.Collectable - pvm.Amnt, 0);

            _context.Loans.Update(loan);
            //_context.Payments.Update(payment);
            _context.SaveChanges();

            LogTransaction(pvm);

            return RedirectToAction("Index", new { id = pvm.Lid });
        }

        private void LogTransaction(PaymentViewModel pvm)
        {
            decimal tAmount = pvm.Amnt;
            var payments = GetPayments(pvm.Lid);

            foreach (var payment in payments)
            {
                if (tAmount <= 0)
                {
                    break;
                }

                decimal amountToLog = Math.Min(tAmount, payment.Collectable);

                if (amountToLog > 0)
                {
                    var transaction = new Transaction
                    {
                        PaymentId = payment.PaymentId,
                        LoanId = pvm.Lid,
                        Amount = amountToLog
                    };

                    _context.Transactions.Add(transaction);
                    tAmount -= amountToLog;
                }
            }

            _context.SaveChanges();
        }

        private List<Payment> GetPayments(int loanID)
        {
            var paymentList =
               from paymentz in _context.Payments.Where(e => e.LoanId == loanID)
               join transaction in _context.Transactions
               on paymentz.PaymentId equals transaction.PaymentId into pGroup
               from transaction in pGroup.DefaultIfEmpty()
               group transaction by paymentz into ptGroup
               select new Payment
               {
                   PaymentId = ptGroup.Key.PaymentId,
                   LoanId = ptGroup.Key.LoanId,
                   ClientId = ptGroup.Key.ClientId,
                   Collectable = ptGroup.Key.Collectable - ptGroup.Sum(t => t.Amount),
                   Date = ptGroup.Key.Date
               };

            return paymentList.ToList(); ;
        }

    }
}