using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IUsersRepository
    {
        Task<PageList<Users>> GetAllAsync(PageParams pageParams, string? search);
        Task<Users> GetByIdAsync(int id);
        Task<List<Users>> GetByEmailAsync(string email);
        Task<Users> CreateAsync(Users user);
        Task<Users> UpdateAsync(Users user);
        Task DeleteAsync(Users user);
    }
}
