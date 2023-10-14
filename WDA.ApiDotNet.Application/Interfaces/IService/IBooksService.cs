using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Services;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IBooksService
    {
        Task<ResultService> CreateAsync(BooksCreateDTO booksDTO);
        Task<ResultService<BooksDTO>> GetAsync(QueryHandler queryHandler);
        Task<ResultService<List<BooksSummaryDTO>>> GetSummaryBooksAsync();
        Task<ResultService> GetByIdAsync(int id);
        Task<ResultService<List<MostRentedBooksDTO>>> GetMostRentedBooks();
        Task<ResultService> UpdateAsync(BooksUpdateDTO booksDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
