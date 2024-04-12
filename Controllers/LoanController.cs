using DinoLoan.Entity;
using DinoLoan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DinoLoan.Controllers
{
    public class LoanController : Controller
    {
        private readonly DinoloanDbContext _context;

        public LoanController(DinoloanDbContext client)
        {
            _context = client;
        }
        public IActionResult Index()
        {
            var clientInfos = (
                from clientInfo in _context.Clientinfos
                join usertype in _context.Usertypes
                on clientInfo.UserType equals usertype.Id
                select new ClientInfoViewModel
                {
                    Id = clientInfo.Id,
                    UserType = clientInfo.UserType,
                    UserTypeName = usertype.Name,
                    FirstName = clientInfo.FirstName,
                    MiddleName = clientInfo.MiddleName,
                    LastName = clientInfo.LastName,
                    Address = clientInfo.Address,
                    ZipCode = clientInfo.ZipCode,
                    Birthday = clientInfo.Birthday,
                    Age = clientInfo.Age,
                    NameOfFather = clientInfo.NameOfFather,
                    NameOfMother = clientInfo.NameOfMother,
                    CivilStatus = clientInfo.CivilStatus,
                    Religion = clientInfo.Religion,
                    Occupation = clientInfo.Occupation,
                }).ToList();
            return View(clientInfos);
        }

        [HttpGet]
        public IActionResult ViewLoan(int id) {
            var loan = _context.Loans.Where(e => e.ClientId == id).ToList();
            if (loan == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = id;
            return View(loan);
        }

        [HttpPost]
        public IActionResult ViewLoan(Loan l) {
             l.Status = "Pending";
            l.Collectable = l.Amount + l.InterestAmount;
            l.DateCreated = DateTime.Now;

            l.DueDate = l.Type switch
            {
                "Daily" => l.DateCreated.AddDays(1),
                "Weekly" => l.DateCreated.AddDays(7),
                "Monthly" => l.DateCreated.AddMonths(1),
                _ => l.DateCreated,
            };
            ;
            l.TotalPayable = l.Collectable;
            _context.Loans.Add(l);
            _context.SaveChanges();

            CreatePaymentSchedule(l);

            return RedirectToAction("ViewLoan", new { id = l.ClientId });
        }

        private void CreatePaymentSchedule(Loan loan)
        {
            int incrementDays = 1;
            switch (loan.Type)
            {
                case "Daily":
                    incrementDays = 1;
                    break;
                case "Weekly":
                    incrementDays = 7;
                    break;
                case "Monthly":
                    incrementDays = 30;
                    break;
            }

            var totalDays = (loan.DueDate - loan.DateCreated).TotalDays;

            if (totalDays <= 0 || incrementDays <= 0)
            {

                throw new InvalidOperationException("Invalid loan dates or increment.");
            }

            var numberOfPayments = (int)(totalDays / incrementDays);

            if (numberOfPayments <= 0)
            {
                throw new InvalidOperationException("Invalid number of payments calculated.");
            }

            var dailyPaymentAmount = loan.Collectable / numberOfPayments;

            var payments = new List<Payment>();

            for (int i = 0; i < numberOfPayments; i++)
            {
                payments.Add(new Payment
                {
                    LoanId = loan.ClientId,
                    Collectable = dailyPaymentAmount,
                    Date = loan.DateCreated.AddDays(i * incrementDays),
                    Status = "Pending"
                });
            }

            _context.Payments.AddRange(payments);
            _context.SaveChanges();
        }
    }
}
