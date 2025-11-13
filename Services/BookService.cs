using bookapi_minimal.Contracts;
using bookapi_minimal.Interfaces;

namespace bookapi_minimal.Services
{
     // Service class for managing books
   public class BookService : IBookService
   {
       // Method to add a new book to the database
       public Task<BookResponse> AddBookAsync(CreateBookRequest createBookRequest)
       {
           throw new NotImplementedException();
       }

      // Method to Delete a book from the database
       public Task<bool> DeleteBookAsync(Guid id)
       {
           throw new NotImplementedException();
       }

       // Method to Get a book from the database by its ID

       public Task<BookResponse> GetBookByIdAsync(Guid id)
       {
           throw new NotImplementedException();
       }

      // Method to Get all books from the database
       public Task<IEnumerable<BookResponse>> GetBooksAsync()
       {
           throw new NotImplementedException();
       }

       // Method to Update a book in the database
       public Task<BookResponse> UpdateBookAsync(Guid id, UpdateBookRequest updateBookRequest)
       {
           throw new NotImplementedException();
       }
   }
}