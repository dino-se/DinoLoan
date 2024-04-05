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

        public IActionResult AddLoan(int id)
        {
            var client = _context.Clientinfos.FirstOrDefault(q => q.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        public IActionResult ViewLoan(int id) {
            var loan = _context.Loans.ToList();
            if (loan == null)
            {
                return NotFound();
            }

            return View(loan);
        }
    }
}
