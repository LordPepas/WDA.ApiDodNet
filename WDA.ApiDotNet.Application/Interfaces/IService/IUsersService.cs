using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;
using WDA.ApiDotNet.Application.Services;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IUsersService
    {
        Task<ResultService> CreateAsync(UsersCreateDTO usersDTO);
        Task<ResultService<UsersDTO>> GetAsync(QueryHandler queryHandler);
        Task<ResultService<List<UsersSummaryDTO>>> GetSummaryUsersAsync();
        Task<ResultService> GetByIdAsync(int id);
        Task<ResultService> UpdateAsync(UsersUpdateDTO usersDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
