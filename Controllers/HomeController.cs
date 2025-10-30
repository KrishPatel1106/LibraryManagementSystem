using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Data;
using System.Linq;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;

        public HomeController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalBooks = _context.Books.Count();
            ViewBag.TotalMembers = _context.Members.Count();
            ViewBag.IssuedCount = _context.Transactions.Count(t => !t.IsReturned);
            ViewBag.ReturnedCount = _context.Transactions.Count(t => t.IsReturned);

            return View();
        }
    }
}
