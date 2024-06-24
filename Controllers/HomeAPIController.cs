using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DinoLoan.Models;
using DinoLoan.Entity;

namespace DinoLoan.Controllers;

public class HomeAPIController : Controller
{
    private readonly DinoloanDbContext _context;

    public HomeAPIController(DinoloanDbContext loan)
    {
        _context = loan;
    }

    public IActionResult ClientCount()
    {
        int clientCount = _context.Clientinfos.Count();
        return Json(new { count = clientCount });
    }

    public IActionResult UserCount()
    {
        int userCount = _context.Accounts.Count();
        return Json(new { count = userCount });
    }

    public IActionResult CollectToday()
        {
            DateTime today = DateTime.Today;
            decimal collectToday = _context.Transactions
                                           .Where(p => p.Date.Date == today)
                                           .Sum(p => p.Amount);
            return Ok(new { collect = collectToday });
        }
}
