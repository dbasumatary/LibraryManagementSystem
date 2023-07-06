using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal class Book
    {
        public int LibraryId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsBorrowed { get; set; }
        public string Borrower { get; set; }

        public override string ToString()
        {
            return $"Book ID: {BookId}\tTitle: {Title}\tAuthor: {Author}\tGenre: {Genre}\tIsBorrowed: {IsBorrowed}\tBorrower: {Borrower}";
        }
    }
}
