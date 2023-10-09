using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDodNet.Application.Repositories.Interface
{
    public interface IBooksRepository
    {
        Task<PageList<Books>> GetAllAsync(PageParams pageParams, string? search);
        Task<Books> GetByIdAsync(int id);
        Task<List<Books>> GetByNameAsync(string name);
        Task<Books> CreateAsync(Books book);
        Task<Books> UpdateAsync(Books book);
        Task DeleteAsync(Books book);
        Task<List<Books>> GetByPublishersIdAsync(int publisherId);
        Task<int> GetTotalCountAsync();
        Task<List<Books>> MostRentedBooks();
    }
}
