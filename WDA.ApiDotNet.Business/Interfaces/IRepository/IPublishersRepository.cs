using WDA.ApiDotNet.Business.Helpers;
using WDA.ApiDotNet.Business.Models;

namespace WDA.ApiDotNet.Business.Interfaces.IRepository
{
    public interface IPublishersRepository
    {
        Task Create(Publishers publisher);
        Task Update(Publishers publisher);
        Task Delete(Publishers publisher);
        Task<PageList<Publishers>> GetAll(QueryHandler queryHandler);
        Task<Publishers> GetById(int? id);
        Task<List<Publishers>> GetSummaryPublishers();
        Task<List<Publishers>> GetByName(string name);
    }
}
