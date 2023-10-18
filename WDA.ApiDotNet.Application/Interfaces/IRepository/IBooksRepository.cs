using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IBooksRepository
    {
        Task Create(Books book);
        Task Update(Books book);
        Task Delete(Books book);
        Task<PageList<Books>> GetAll(QueryHandler queryHandler);
        Task<Books> GetById(int? id);
        Task<List<Books>> GetSummaryBooks();
        Task<List<Books>> GetSummaryAvailableBooks();
        Task<List<Books>> GetByName(string name);
        Task<List<Books>> GetByPublishersId(int publisherId);
        Task<List<Books>> MostRentedBooks();
    }
}
