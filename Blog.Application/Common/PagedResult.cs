namespace Blog.Application.Common
{
    public abstract class PagedResult : BaseResult
    {
        protected PagedResult(ResultType status) : base(status)
        {

        }

        public int PageNumber { get; set; }
        public int PagesCount { get; set; }
    }
}