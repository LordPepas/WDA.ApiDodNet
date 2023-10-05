using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.Helpers;
using System.Collections.Generic;

namespace WDA.ApiDodNet.Application.Repositories.Interface
{
    public interface IPublishersRepository
    {
        Task<PageList<Publishers>> GetAllAsync(PageParams pageParams, string? value);
        Task<Publishers> GetByIdAsync(int id);
        Task<List<Publishers>> GetByNameAsync(string name);
        Task<Publishers> CreateAsync(Publishers publisher);
        Task<Publishers> UpdateAsync(Publishers publisher);
        Task DeleteAsync(Publishers publisher);
    }
}
