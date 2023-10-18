using Microsoft.EntityFrameworkCore;
namespace WDA.ApiDotNet.Application.Helpers
{
    public class PageList<T> : List<T>
    {
        public int PageNumber { get; set; }
        public int ItemsPerpage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data => this;

        public PageList(List<T> items, int pageNumber, int itemsPerPage, int count)
        {
            TotalCount = count;
            ItemsPerpage = itemsPerPage;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)itemsPerPage);
            this.AddRange(items);
        }

        public static async Task<PageList<T>> GetResponseAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();

            var items = await source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();
            return new PageList<T>(items, pageNumber, pageSize, count);
        }
    }
}
