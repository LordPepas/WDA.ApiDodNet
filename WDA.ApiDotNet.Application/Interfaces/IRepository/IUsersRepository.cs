using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IUsersRepository
    {
        Task Create(Users user);
        Task Update(Users user);
        Task Delete(Users user);
        Task<PageList<Users>> GetAll(QueryHandler queryHandler);
        Task<Users> GetById(int? id);
        Task<List<Users>> GetSummaryUsers();
        Task<List<Users>> GetByEmail(string email);
    }
}
