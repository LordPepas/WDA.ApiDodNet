using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IUsersRepository
    {
        Task CreateAsync(Users user);
        Task UpdateAsync(Users user);
        Task<Users> GetByIdAsync(int id);
        Task<PageList<Users>> GetAllAsync(PageParams pageParams, string? search);
        Task<List<Users>> GetSummaryUsersAsync();
        Task DeleteAsync(Users user);
        Task<List<Users>> GetByEmailAsync(string email);
    }
}
