using Microsoft.AspNetCore.Mvc;

namespace DTO_Pagination_Filtering_Mapping;

[ApiController]
[Route("/api/books")]
public sealed class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetBooks([FromQuery] BookFilter filter)
    {
        var result = _bookRepository.GetBooks(filter);
        return Ok(ApiResponse<PaginationResponse<IEnumerable<BookReadDto>>>.Success(null!, result));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetBookById(int id)
    {
        var book = _bookRepository.GetBookById(id);
        return book != null
            ? Ok(ApiResponse<BookReadDto>.Success(null!, book))
            : NotFound(ApiResponse<BookReadDto>.Fail(null!, null));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateBook([FromBody] BookCreateDto bookCreateDto)
    {
        var result = _bookRepository.CreateBook(bookCreateDto);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : BadRequest(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateBook([FromBody] BookUpdateDto bookUpdateDto)
    {
        var result = _bookRepository.UpdateBook(bookUpdateDto);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : NotFound(ApiResponse<bool>.Fail(null!, result));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteBook(int id)
    {
        var result = _bookRepository.DeletedBook(id);
        return result
            ? Ok(ApiResponse<bool>.Success(null!, result))
            : NotFound(ApiResponse<bool>.Fail(null!, result));
    }
}
