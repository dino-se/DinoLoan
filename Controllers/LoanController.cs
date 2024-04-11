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
                "Daily" => (DateTime?)l.DateCreated.AddDays(1),
                "Weekly" => (DateTime?)l.DateCreated.AddDays(7),
                "Monthly" => (DateTime?)l.DateCreated.AddMonths(1),
                _ => (DateTime?)l.DateCreated,
            };
            l.TotalPayable = l.Collectable;
            _context.Loans.Add(l);
            _context.SaveChanges();

            return RedirectToAction("ViewLoan", new { id = l.ClientId });
        }
    }
}
