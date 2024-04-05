using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DinoLoan.Models;
using DinoLoan.Entity;

namespace DinoLoan.Controllers;

public class LoanAPIController : Controller
{
    private readonly DinoloanDbContext _context;

    public LoanAPIController(DinoloanDbContext loan)
    {
        _context = loan;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CreateLoan(Loan l)
    {
        _context.Loans.Add(l);
        _context.SaveChanges();
        return Ok();
    }
}
