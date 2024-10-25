public record BaseFilter
{
    public int PageNumber{get; init;}
    public int PageSize{get; init;}

    public BaseFilter()
    {
        this.PageSize=5;
        this.PageNumber=1;
    }

    public BaseFilter(int pageSize, int pageNumber)
    {
        if(pageSize<=0) this.PageSize=5;
        this.PageSize=pageSize;
        if(pageNumber<=0) this.PageNumber=1;
        this.PageNumber=pageNumber;
    }
}