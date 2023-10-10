using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IPublishersRepository
    {
        Task<PageList<Publishers>> GetAllAsync(PageParams pageParams, string? search);
        Task<Publishers> GetByIdAsync(int id);
        Task<List<Publishers>> GetByNameAsync(string name);
        Task<Publishers> CreateAsync(Publishers publisher);
        Task<Publishers> UpdateAsync(Publishers publisher);
        Task DeleteAsync(Publishers publisher);
    }
}
