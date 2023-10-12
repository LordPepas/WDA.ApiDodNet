using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IBooksRepository
    {
        Task CreateAsync(Books book);
        Task UpdateAsync(Books book);
        Task<PageList<Books>> GetAllAsync(PageParams pageParams, string? search);
        Task<Books> GetByIdAsync(int id);
        Task<List<Books>> GetSummaryBooksAsync();
        Task DeleteAsync(Books book);
        Task<List<Books>> GetByNameAsync(string name);
        Task<List<Books>> GetByPublishersIdAsync(int publisherId);
        Task<List<Books>> MostRentedBooks();
    }
}
