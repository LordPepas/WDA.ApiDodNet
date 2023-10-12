using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IPublishersRepository
    {
        Task CreateAsync(Publishers publisher);
        Task UpdateAsync(Publishers publisher);
        Task<Publishers> GetByIdAsync(int id);
        Task<PageList<Publishers>> GetAllAsync(PageParams pageParams, string? search);
        Task DeleteAsync(Publishers publisher);
        Task<List<Publishers>> GetSummaryPublishersAsync();
        Task<List<Publishers>> GetByNameAsync(string name);
    }
}
