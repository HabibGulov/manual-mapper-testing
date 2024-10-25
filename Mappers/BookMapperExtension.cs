public static class CourseMapperExtension
{
    public static BookReadDto BookToBookRead(this Book book)
    {
        return new BookReadDto()
        {
            Id = book.Id,
            Name = book.Name,
            Description = book.Description
        };
    }

    public static Book BookUpdateDto(this Book book, BookUpdateDto bookUpdateDto)
    {
        book.Id = bookUpdateDto.Id;
        book.Name = bookUpdateDto.Name;
        book.Description = bookUpdateDto.Description;
        book.UpdatedAt = DateTime.UtcNow;
        book.Version += 1;
        return book;
    }

    public static Book BookCreateToBook(this BookCreateDto bookCreateDto, BookDbContext bookDbContext)
    {
        int maxId = bookDbContext.Books.Where(x=>x.IsDeleted==false).Any() ? bookDbContext.Books.Where(x=>x.IsDeleted==false).Max(x => x.Id) + 1 : 1;

        return new Book()
        {

            Id = maxId,
            Name = bookCreateDto.Name,
            Description = bookCreateDto.Description,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Book Deletebook(this Book book)
    {
        book.IsDeleted = true;
        book.DeletedAt = DateTime.UtcNow;
        book.UpdatedAt = DateTime.UtcNow;
        book.Version += 1;

        return book;
    }
}