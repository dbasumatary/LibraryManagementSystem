namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Library Management System");
            
            Library libraries = new Library("Library1");
            
            bool flag = true;
            while( flag)
            {
                Console.WriteLine("\nPLease enter the choices");
                Console.WriteLine("\n1. Add New Books\n2. Get Total Books\n3. Available Books\n4. Borrowed Books\n5. Get Books By Author\n6. Get Books By Genre\n7. Get Book Details\n8. Borrow Book\n9. Return Book\n10. Exit");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            libraries.AddBook(new Book { BookId = 1, Title = "Game Of Thrones", Author = "George Martin", Genre = "Drama", IsBorrowed = true, Borrower = "David" });
                            libraries.AddBook(new Book { BookId = 2, Title = "Percy Jackson", Author = "Rick Riordan", Genre = "Adventure", IsBorrowed = true, Borrower = "Sudheer" });
                            libraries.AddBook(new Book { BookId = 3, Title = "Angels And Demons", Author = "Dan Brown", Genre = "Thriller", IsBorrowed = true, Borrower = "Kundan" });
                            libraries.AddBook(new Book { BookId = 4, Title = "Lord Of the Rings", Author = "JRR Tolkien", Genre = "Adventure", IsBorrowed = true, Borrower = "Rohith" });
                            libraries.AddLibrary(libraries);
                            break;

                        case 2:
                            int totalBooks = libraries.GetTotalBooks();
                            Console.WriteLine($"Total books: {totalBooks}");
                            break;

                        case 3:
                            int availableBooks = libraries.GetAvailableBooks();
                            Console.WriteLine($"Available books: {availableBooks}");
                            break;

                        case 4:
                            int borrowedBooks = libraries.GetBorrowedBooks();
                            Console.WriteLine($"Borrowed books: {borrowedBooks}");
                            break;

                        case 5:
                            Console.Write("Enter the author name: ");
                            string authorInput = Console.ReadLine();
                            libraries.GetBooksByAuthor(authorInput);
                            break;

                        case 6:
                            Console.Write("Enter the book genre: ");
                            string genreInput = Console.ReadLine();
                            libraries.GetBooksByGenre(genreInput);
                            break;

                        case 7:
                            Console.Write("Enter the book Id: ");
                            int displayInput = Convert.ToInt32(Console.ReadLine());
                            libraries.GetBookDetails(displayInput);
                            break;

                        case 8:
                            Console.Write("Enter the book Id: ");
                            int borrowInputId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("\nEnter the borrower name: ");
                            string borrowerName = Console.ReadLine();
                            libraries.BorrowBook(borrowInputId, borrowerName);
                            break;

                        case 9:
                            Console.Write("Enter the book Id: ");
                            int returnInput = Convert.ToInt32(Console.ReadLine());
                            libraries.ReturnBook(returnInput);
                            break;

                        default:
                            flag = false;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("General exception caught: " + e.Message);
                }
            }
        }
    }
}