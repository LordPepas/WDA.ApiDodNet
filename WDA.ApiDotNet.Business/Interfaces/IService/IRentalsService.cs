using WDA.ApiDotNet.Business.Helpers;
using WDA.ApiDotNet.Business.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Business.Services;

namespace WDA.ApiDotNet.Business.Interfaces.IServices
{
    public interface IRentalsService
    {
        Task<ResultService> CreateAsync(RentalsCreateDTO rentalsDTO);
        Task<ResultService<RentalsDTO>> GetAsync(QueryHandler queryHandler);
        Task<ResultService<RentalsDTO>> GetByIdAsync(int id);
        Task<ResultService> UpdateAsync(int id);
        Task<ResultService> DeleteAsync(int id);
    }
}
