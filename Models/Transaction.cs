using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required]
        [ForeignKey("Member")]
        public int MemberId { get; set; }

        [Required]
        public DateTime IssueDate { get; set; } = DateTime.Now;

        public DateTime? ReturnDate { get; set; }

        public bool IsReturned { get; set; } = false;

        // Navigation properties
        public Book? Book { get; set; }
        public Member? Member { get; set; }
    }
}
