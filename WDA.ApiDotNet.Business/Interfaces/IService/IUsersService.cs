using WDA.ApiDotNet.Business.Helpers;
using WDA.ApiDotNet.Business.Models.DTOs.UsersDTO;
using WDA.ApiDotNet.Business.Services;

namespace WDA.ApiDotNet.Business.Interfaces.IServices
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
