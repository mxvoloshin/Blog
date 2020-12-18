namespace Blog.Application.Common
{
    public abstract class PagedQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}