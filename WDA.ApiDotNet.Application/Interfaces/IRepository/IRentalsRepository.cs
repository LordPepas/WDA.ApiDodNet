using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models;

namespace WDA.ApiDotNet.Application.Interfaces.IRepository
{
    public interface IRentalsRepository
    {
        Task Create(Rentals rental);
        Task Update(Rentals rental);
        Task Delete(Rentals rental);
        Task<Rentals> GetById(int? id);
        Task<PageList<Rentals>> GetAll(QueryHandler queryHandler);
        Task<List<Rentals>> GetByUserId(int userId);
        Task<List<Rentals>> GetByBookId(int bookId);
        Task<List<Rentals>> GetRentalByUserIdandBookId(int bookId, int userId);

    }
}
