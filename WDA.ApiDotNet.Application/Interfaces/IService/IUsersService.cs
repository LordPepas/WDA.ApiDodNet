using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;
using WDA.ApiDotNet.Application.Services;
using WDA.ApiDotNet.Business.Helpers;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IUsersService
    {
        Task<ResultService> CreateAsync(UsersCreateDTO usersDTO);
        Task<ResultService<PaginationResponse<UsersDTO>>> GetAsync(PageParams pageParams, string? search);
        Task<ResultService<List<UserRentalDTO>>> GetSummaryUsersAsync();
        Task<ResultService> GetByIdAsync(int id);
        Task<ResultService> UpdateAsync(UsersUpdateDTO usersDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
