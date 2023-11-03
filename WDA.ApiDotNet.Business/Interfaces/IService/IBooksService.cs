using WDA.ApiDotNet.Business.Helpers;
using WDA.ApiDotNet.Business.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Business.Services;

namespace WDA.ApiDotNet.Business.Interfaces.IServices
{
    public interface IBooksService
    {
        Task<ResultService> CreateAsync(BooksCreateDTO booksDTO);
        Task<ResultService<BooksDTO>> GetAsync(QueryHandler queryHandler);
        Task<ResultService<List<BooksSummaryDTO>>> GetSummaryBooksAsync();
        Task<ResultService<List<BooksAvailableDTO>>> GetSummaryAvailableBooksAsync();
        Task<ResultService> GetByIdAsync(int id);
        Task<ResultService<List<MostRentedBooksDTO>>> GetMostRentedBooks();
        Task<ResultService> UpdateAsync(BooksUpdateDTO booksDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
