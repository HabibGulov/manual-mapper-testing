public interface IBookRepository
{
    PaginationResponse<IEnumerable<BookReadDto>> GetBooks(BookFilter bookFilter);
    BookReadDto? GetBookById(int id);
    bool CreateBook(BookCreateDto bookCreateDto);
    bool UpdateBook(BookUpdateDto bookUpdateDto);
    bool DeletedBook(int id);
}