namespace WDA.ApiDotNet.Application.Helpers
{
    public class Filters
    {
        public string? SearchValue { get; set; }
        public string? OrderBy { get; set; }
    }

    public class PageParams
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }

    public class QueryHandler
    {
        public Filters Filter { get; set; }
        public PageParams Paging { get; set; }

        public QueryHandler()
        {
            Filter = new Filters();
            Paging = new PageParams();
        }
    }
}
