using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Services;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IRentalsService
    {
        Task<ResultService<ICollection<RentalsDTO>>> GetAsync(PageParams pageParams, string? search);
        Task<ResultService<RentalsDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(RentalsCreateDTO rentalsDTO);
        Task<ResultService> UpdateAsync(RentalsUpdateDTO rentalsUpdateDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<List<BookRentalDTO>>> GetSelectBooksAsync();
        Task<ResultService<List<UserRentalDTO>>> GetSelectUsersAsync();
    }
}
