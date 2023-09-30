using WDA.ApiDodNet.Data.Models;

namespace WDA.ApiDodNet.Data.Repositories.Interface
{
    public interface IBooksRepository
    {
        Task<Books> GetByIdAsync(int id);
        Task<ICollection<Books>> GetByBooksAsync();
        Task<List<Books>> GetByNameAsync(string name);
        Task<Books> CreateAsync(Books book);
        Task<Books> UpdateAsync(Books book);
        Task DeleteAsync(Books book);
        Task<List<Books>> GetByPublishersIdAsync(int publisherId);
    }
}
