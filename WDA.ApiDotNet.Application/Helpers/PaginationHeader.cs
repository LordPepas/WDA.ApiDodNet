
namespace WDA.ApiDotNet.Application.Helpers
{
    public class PaginationHeader<T>
    {
        public PaginationHeader(int currentPage, int itemsPerpage, int totalitems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemsPerpage = itemsPerpage;
            TotalItems = totalitems;
            TotalPages = totalPages;
        }

        public int CurrentPage { get; set; }
        public int ItemsPerpage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}