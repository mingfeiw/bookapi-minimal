using bookapi_minimal.AppContext;
using bookapi_minimal.Contracts;
using bookapi_minimal.Interfaces;
using bookapi_minimal.Models;
using Microsoft.EntityFrameworkCore;

namespace bookapi_minimal.Services
{
    // Service class for managing books
    public class BookService : IBookService
    {
        private readonly ApplicationContext _context;

        public BookService(ApplicationContext context)
        {
            _context = context;
        }

        // Method to add a new book to the database
        public async Task<BookResponse> AddBookAsync(CreateBookRequest createBookRequest)
        {
            var book = new BookModel
            {
                Id = Guid.NewGuid(),
                Title = createBookRequest.Title,
                Author = createBookRequest.Author,
                Description = createBookRequest.Description,
                Category = createBookRequest.Category,
                Language = createBookRequest.Language,
                TotalPages = createBookRequest.TotalPages
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Category = book.Category,
                Language = book.Language,
                TotalPages = book.TotalPages
            };
        }

        // Method to Delete a book from the database
        public async Task<bool> DeleteBookAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }

        // Method to Get a book from the database by its ID
        public async Task<BookResponse> GetBookByIdAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return null;

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Category = book.Category,
                Language = book.Language,
                TotalPages = book.TotalPages
            };
        }

        // Method to Get all books from the database
        public async Task<IEnumerable<BookResponse>> GetBooksAsync()
        {
            var books = await _context.Books.ToListAsync();
            return books.Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                Description = b.Description,
                Category = b.Category,
                Language = b.Language,
                TotalPages = b.TotalPages
            });
        }

        // Method to Update a book in the database
        public async Task<BookResponse> UpdateBookAsync(Guid id, UpdateBookRequest updateBookRequest)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
                return null;

            book.Title = updateBookRequest.Title;
            book.Author = updateBookRequest.Author;
            book.Description = updateBookRequest.Description;
            book.Category = updateBookRequest.Category;
            book.Language = updateBookRequest.Language;
            book.TotalPages = updateBookRequest.TotalPages;

            await _context.SaveChangesAsync();

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Description = book.Description,
                Category = book.Category,
                Language = book.Language,
                TotalPages = book.TotalPages
            };
        }
    }
}