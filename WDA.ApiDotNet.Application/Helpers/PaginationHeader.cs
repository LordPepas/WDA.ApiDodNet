namespace WDA.ApiDotNet.Application.Helpers
{
    public class PaginationHeader<T>
    {
        public PaginationHeader(int pageNumber , int itemsPerpage, int totalitems, int totalPages)
        {
            PageNumber = pageNumber;
            ItemsPerpage = itemsPerpage;
            TotalItems = totalitems;
            TotalPages = totalPages;
        }

        public int PageNumber { get; set; }
        public int ItemsPerpage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}