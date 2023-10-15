namespace WDA.ApiDotNet.Application.Helpers
{

    public class QueryHandler
    {
        public string? SearchValue { get; set; }
        public string? OrderBy { get; set; }
        public bool OrderDesc { get; set; }
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
    }
}
