using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IRentalsRepository
    {
        Task CreateAsync(Rentals rental);
        Task UpdateAsync(Rentals rental);
        Task<Rentals> GetByIdAsync(int id);
        Task<PageList<Rentals>> GetAllAsync(PageParams pageParams, string? search);
        Task DeleteAsync(Rentals rental);
        Task<List<Rentals>> GetByUserIdAsync(int userId);
        Task<List<Rentals>> GetByBookIdAsync(int bookId);
        Task<bool> CheckDate(DateTime rentalDate);
        Task<bool?> CheckPrevisionDate(DateTime forecastDate, DateTime rentalDate);
        Task<bool> GetStatus(DateTime forecastDate, DateTime returnDate);
    }
}
