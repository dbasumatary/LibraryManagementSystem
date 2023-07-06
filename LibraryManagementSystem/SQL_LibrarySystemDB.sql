CREATE DATABASE LibrarySystemDB;
USE LibrarySystemDB;

CREATE TABLE Library (
    LibraryId INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE Book (
    BookId INT NOT NULL,
    Title VARCHAR(100) NOT NULL,
    Author VARCHAR(100) NOT NULL,
    Genre VARCHAR(50) NOT NULL,
    IsBorrowed BIT NOT NULL,
	Borrower VARCHAR(50)
);

SELECT * FROM Library;
SELECT * FROM Book;

drop table Book;
drop table Library;

-- AddBook
GO
CREATE PROCEDURE AddBook
	@BookId INT,
    @Title NVARCHAR(100),
    @Author NVARCHAR(100),
    @Genre NVARCHAR(100),
    @IsBorrowed BIT,
    @Borrower NVARCHAR(100)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Book WHERE Title = @Title)
    BEGIN
        SELECT 'Book already exists in the library.';
        RETURN;
    END

    INSERT INTO Book (BookId, Title, Author, Genre, IsBorrowed, Borrower)
    VALUES (@BookId, @Title, @Author, @Genre, @IsBorrowed, @Borrower);

    SELECT 'Book added successfully.';
END
GO


GO

-- AddLibrary
CREATE PROCEDURE AddLibrary
    @Name NVARCHAR(100)
AS
BEGIN
    INSERT INTO Library (Name)
    VALUES (@Name)
END
GO

-- GetTotalBooks
CREATE PROCEDURE GetTotalBooks
AS
BEGIN
    SELECT COUNT(*) AS TotalBooks FROM Book
END
GO

-- GetAvailableBooks
CREATE PROCEDURE GetAvailableBooks
AS
BEGIN
    SELECT COUNT(*) AS AvailableBooks FROM Book WHERE IsBorrowed = 0
END
GO

-- GetBorrowedBooks
CREATE PROCEDURE GetBorrowedBooks
AS
BEGIN
    SELECT COUNT(*) AS BorrowedBooks FROM Book WHERE IsBorrowed = 1
END
GO

-- GetBooksByAuthor
CREATE PROCEDURE GetBooksByAuthor
    @Author NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Book WHERE Author = @Author
END
GO

-- GetBooksByGenre
CREATE PROCEDURE GetBooksByGenre
    @Genre NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Book WHERE Genre = @Genre
END
GO

-- BorrowBook
CREATE PROCEDURE BorrowBook
    @BookId INT,
    @Borrower NVARCHAR(100)
AS
BEGIN
    UPDATE Book SET IsBorrowed = 1, Borrower = @Borrower WHERE BookId = @BookId
END
GO

-- ReturnBook
CREATE PROCEDURE ReturnBook
    @BookId INT
AS
BEGIN
    UPDATE Book SET IsBorrowed = 0, Borrower = NULL WHERE BookId = @BookId
END
GO

-- GetBookDetails
CREATE PROCEDURE GetBookDetails
    @BookId INT
AS
BEGIN
    SELECT * FROM Book WHERE BookId = @BookId
END
GO


--drop procedure dbo.AddBook, dbo.AddLibrary, dbo.GetAvailableBooks, dbo.GetBorrowedBooks, dbo.GetTotalBooks, dbo.GetBooksByAuthor,dbo.GetBooksByGenre,dbo.GetBookDetails,dbo.ReturnBook,dbo.BorrowBook;
--drop procedure dbo.AddBook;
GO