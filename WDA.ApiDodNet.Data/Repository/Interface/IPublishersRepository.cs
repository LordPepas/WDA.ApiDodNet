using WDA.ApiDodNet.Data.Models;

namespace WDA.ApiDodNet.Data.Repositories.Interface
{
    public interface IPublishersRepository
    {
        Task<ICollection<Publishers>>GetByIdAsync();
        Task<Publishers> GetByIdAsync(int id);
        Task<List<Publishers>> GetByNameAsync(string name);
        Task<Publishers> CreateAsync(Publishers publisher);
        Task<Publishers> UpdateAsync(Publishers publisher);
        Task DeleteAsync(Publishers publisher);
    }
}
