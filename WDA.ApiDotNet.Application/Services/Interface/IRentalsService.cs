using WDA.ApiDotNet.Application.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDotNet.Application.Services.Interface
{
    public interface IRentalsService
    {
        Task<ResultService<ICollection<RentalsDTO>>> GetAsync(PageParams pageParams , string? search);
        Task<ResultService<RentalsDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(RentalsCreateDTO rentalsDTO);
        Task<ResultService> UpdateAsync(RentalsUpdateDTO rentalsUpdateDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
