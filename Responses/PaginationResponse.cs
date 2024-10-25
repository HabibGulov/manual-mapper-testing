public record PaginationResponse<T> : BaseFilter
{
    public int TotalPages { get; init; }
    public int TotalRecords { get; init; }
    public T? Data{get; init;}

    public PaginationResponse(int pageNumber, int pageSize, int totalRecords, T data):base(pageSize, pageNumber)
    {
        this.TotalRecords=totalRecords;
        this.TotalPages=(int)Math.Ceiling((double)(TotalRecords/pageSize));
        this.Data=data;
    }

    public static PaginationResponse<T> Create(int pageNumber, int pageSize, int totalRecords, T data)
    => new PaginationResponse<T>(pageNumber, pageSize, totalRecords, data);
}