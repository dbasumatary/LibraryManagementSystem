using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem
{
    internal class Library
    {
        private string connectionString = "Data Source=DESKTOP-T464DC4;Initial Catalog=LibrarySystemDB;Integrated Security=True;";

        public string Name { get; set; }

        public Library(string name)
        {
            this.Name = name;
        }

        public void AddBook(Book book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AddBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", book.BookId);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@Genre", book.Genre);
                    command.Parameters.AddWithValue("@IsBorrowed", book.IsBorrowed);
                    command.Parameters.AddWithValue("@Borrower", book.Borrower);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine(reader.GetString(0));
                        }
                    }
                }
            }
        }

        public void AddLibrary(Library library)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AddLibrary", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", library.Name);

                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Library added to the database.");
        }

        public int GetTotalBooks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetTotalBooks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public int GetAvailableBooks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetAvailableBooks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public int GetBorrowedBooks()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetBorrowedBooks", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    return (int)command.ExecuteScalar();
                }
            }
        }

        public void PrintBookDetails(Book book)
        {
            if (book != null)
            {
                Console.WriteLine($"Book ID: {book.BookId}");
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Genre: {book.Genre}");
                Console.WriteLine($"IsBorrowed: {book.IsBorrowed}");
                Console.WriteLine($"Borrower: {book.Borrower}");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }


        public Book GetBooksByAuthor(string author)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetBooksByAuthor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Author", author);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Book book = new Book
                            {
                                BookId = (int)reader["BookId"],
                                Title = (string)reader["Title"],
                                Author = (string)reader["Author"],
                                Genre = (string)reader["Genre"],
                                IsBorrowed = (bool)reader["IsBorrowed"],
                                Borrower = (string)reader["Borrower"]
                            };

                            Console.WriteLine("The book details by author: ");
                            PrintBookDetails(book);
                        }
                        else
                        {
                            Console.WriteLine("Book not found.");
                        }
                    }
                }
            }

            return null;
        }

        public Book GetBooksByGenre(string genre)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetBooksByGenre", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Genre", genre);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Book book = new Book
                            {
                                BookId = (int)reader["BookId"],
                                Title = (string)reader["Title"],
                                Author = (string)reader["Author"],
                                Genre = (string)reader["Genre"],
                                IsBorrowed = (bool)reader["IsBorrowed"],
                                Borrower = (string)reader["Borrower"]
                            };
                            Console.WriteLine("The book details by genre: ");
                            PrintBookDetails(book);
                        }
                        else { Console.WriteLine("Book not found."); }
                    }
                }
            }
            return null;
        }

        public void BorrowBook(int bookId, string borrower)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("BorrowBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", bookId);
                    command.Parameters.AddWithValue("@Borrower", borrower);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Book borrowed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Book not found or already borrowed.");
                    }
                }
            }
        }

        public void ReturnBook(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ReturnBook", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", bookId);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Book returned successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Book not found or not borrowed.");
                    }
                }
            }
        }

        public Book GetBookDetails(int bookId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetBookDetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", bookId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Book book = new Book
                            {
                                BookId = (int)reader["BookId"],
                                Title = (string)reader["Title"],
                                Author = (string)reader["Author"],
                                Genre = (string)reader["Genre"],
                                IsBorrowed = (bool)reader["IsBorrowed"],
                                Borrower = (string)reader["Borrower"]
                            };
                            Console.WriteLine("The book details by book id: ");
                            PrintBookDetails(book);
                        }
                        else { Console.WriteLine("Book not found."); }
                    }
                }
            }

            return null;
        }
    }
}
