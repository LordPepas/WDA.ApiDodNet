using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDodNet.Application.Repositories.Interface
{
    public interface IUsersRepository
    {
        Task<PageList<Users>> GetAllAsync(PageParams pageParams, string? value);
        Task<Users> GetByIdAsync(int id);
        Task<List<Users>> GetByEmailAsync(string email);
        Task<Users> CreateAsync(Users user);
        Task<Users> UpdateAsync(Users user);
        Task DeleteAsync(Users user);
    }
}
