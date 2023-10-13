using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Services;
using WDA.ApiDotNet.Business.Helpers;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IRentalsService
    {
        Task<ResultService> CreateAsync(RentalsCreateDTO rentalsDTO);
        Task<ResultService<PaginationResponse<RentalsDTO>>> GetAsync(QueryHandler queryHandler);
        Task<ResultService<RentalsDTO>> GetByIdAsync(int id);
        Task<ResultService> UpdateAsync(RentalsUpdateDTO rentalsUpdateDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
