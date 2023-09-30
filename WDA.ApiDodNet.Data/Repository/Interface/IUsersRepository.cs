using WDA.ApiDodNet.Data.Models;

namespace WDA.ApiDodNet.Data.Repositories.Interface
{
    public interface IUsersRepository
    {
        Task<Users> GetByIdAsync(int id);
        Task<List<Users>> GetByEmailAsync(string email);
        Task<ICollection<Users>>GetByUsersAsync();
        Task<Users> CreateAsync(Users user);
        Task<Users> UpdateAsync(Users user);
        Task DeleteAsync(Users user);
    }
}
