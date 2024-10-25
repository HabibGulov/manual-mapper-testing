
public class BookRepository(BookDbContext context) : IBookRepository
{
    public bool CreateBook(BookCreateDto bookCreateDto)
    {
        try
        {
            bool isExisted = context.Books.Any(x=>x.Name.ToLower()==bookCreateDto.Name.ToLower() && x.IsDeleted==false);
            if(isExisted==true) return false;
            // Book book = bookCreateDto.BookCreateToBook();
            context.Books.Add(bookCreateDto.BookCreateToBook(context));
            context.SaveChanges();
            return true;
        }   
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public bool DeletedBook(int id)
    {
        try
        {
            Book? book = context.Books.FirstOrDefault(x=>x.Id==x.Id && x.IsDeleted==false);
            if(book==null) return false;
            book.Deletebook();
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }

    public BookReadDto? GetBookById(int id)
    {
        try
        {
            Book? book = context.Books.FirstOrDefault(x=>x.Id==id && x.IsDeleted==false);
            BookReadDto? bookReadDto = book?.BookToBookRead();
            return bookReadDto;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return new BookReadDto();
        }
    }

    public PaginationResponse<IEnumerable<BookReadDto>> GetBooks(BookFilter bookFilter)
    {
        try
        {
            IQueryable<BookReadDto> books = context.Books.Where(x=>x.IsDeleted==false).Select(x=>x.BookToBookRead());
            
            if(bookFilter.Name!=null) books = books.Where(x=>x.Name.ToLower().Contains(bookFilter.Name.ToLower()));
            
            books = books.Skip((bookFilter.PageNumber-1)*bookFilter.PageSize).Take(bookFilter.PageSize);

            int totalRecords = context.Books.Where(x=>x.IsDeleted==false).Count();

            return PaginationResponse<IEnumerable<BookReadDto>>.Create(bookFilter.PageNumber, bookFilter.PageSize, totalRecords, books);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return new PaginationResponse<IEnumerable<BookReadDto>>(
                pageNumber: bookFilter.PageNumber,
                pageSize: bookFilter.PageSize,
                totalRecords: 0,
                data: Enumerable.Empty<BookReadDto>()
            );
        }
    }

    public bool UpdateBook(BookUpdateDto bookUpdateDto)
    {
        try
        {
            Book? book = context.Books.FirstOrDefault(x=>x.Id==bookUpdateDto.Id && x.IsDeleted==false);
            if(book==null) return false;
            book.BookUpdateDto(bookUpdateDto);
            context.SaveChanges();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            return false;
        }
    }
}