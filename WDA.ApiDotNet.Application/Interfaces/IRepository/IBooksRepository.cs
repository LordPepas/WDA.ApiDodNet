using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
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
        Task<List<Books>> MostRentedBooks();
        Task<List<Publishers>> GetSelectPublishersAsync();
    }
}
