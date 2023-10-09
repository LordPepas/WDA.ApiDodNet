using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDodNet.Application.Repositories.Interface
{
    public interface IRentalsRepository
    {
        Task<PageList<Rentals>> GetAllAsync(PageParams pageParams, string? search);
        Task<Rentals> GetByIdAsync(int id);
        Task<Rentals> CreateAsync(Rentals rental);
        Task<Rentals> UpdateAsync(Rentals rental);
        Task DeleteAsync(Rentals rental);
        Task<List<Rentals>> GetByUserIdAsync(int userId);
        Task<List<Rentals>> GetByBookIdAsync(int bookId);
        Task<bool> CheckDate(DateTime rentalDate);
        Task<bool?> CheckPrevisionDate(DateTime forecastDate, DateTime rentalDate);
        Task<bool> GetStatus(DateTime forecastDate, DateTime returnDate);
        Task<int> GetTotalCountAsync();
    }
}
