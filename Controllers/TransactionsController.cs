using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using System.Linq;

namespace LibraryManagementSystem.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly LibraryContext _context;

        public TransactionsController(LibraryContext context)
        {
            _context = context;
        }

        // GET: /Transactions
        public IActionResult Index()
        {
            var transactions = _context.Transactions
                .Include(t => t.Book)
                .Include(t => t.Member)
                .ToList();

            return View(transactions);
        }

        // GET: /Transactions/Issue
        public IActionResult Issue()
        {
            ViewBag.Books = _context.Books.Where(b => b.IsAvailable).ToList();
            ViewBag.Members = _context.Members.ToList();
            return View();
        }

        // POST: /Transactions/Issue
        [HttpPost]
        public IActionResult Issue(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                var book = _context.Books.Find(transaction.BookId);
                if (book != null)
                {
                    book.IsAvailable = false;
                    transaction.IssueDate = DateTime.Now;
                    transaction.IsReturned = false;

                    _context.Transactions.Add(transaction);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Books = _context.Books.Where(b => b.IsAvailable).ToList();
            ViewBag.Members = _context.Members.ToList();
            return View(transaction);
        }

        // GET: /Transactions/Return/{id}
        public IActionResult Return(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Book)
                .Include(t => t.Member)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        // POST: /Transactions/Return
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ReturnConfirmed(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Book)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            transaction.IsReturned = true;
            transaction.ReturnDate = DateTime.Now;

            if (transaction.Book != null)
            {
                transaction.Book.IsAvailable = true;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
