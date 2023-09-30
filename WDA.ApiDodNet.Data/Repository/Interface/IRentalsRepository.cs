using WDA.ApiDodNet.Data.Models;

namespace WDA.ApiDodNet.Data.Repositories.Interface
{
    public interface IRentalsRepository
    {
        Task<Rentals> GetByIdAsync(int id);
        Task<ICollection<Rentals>> GetByRentalsAsync();
        Task<Rentals> CreateAsync(Rentals rental);
        Task<Rentals> UpdateAsync(Rentals rental);
        Task DeleteAsync(Rentals rental);
        Task<List<Rentals>> GetByUserIdAsync(int userId);
        Task<List<Rentals>> GetByBookIdAsync(int bookId);
    }
}
