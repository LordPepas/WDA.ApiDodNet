using WDA.ApiDotNet.Business.Helpers;
using WDA.ApiDotNet.Business.Models;

namespace WDA.ApiDotNet.Business.Interfaces.IRepository
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
        Task<List<Books>> GetByNameAndPublisher(string name, int? publisherId);
        Task<List<Books>> GetByPublishersId(int publisherId);
        Task<List<Books>> MostRentedBooks();
    }
}
