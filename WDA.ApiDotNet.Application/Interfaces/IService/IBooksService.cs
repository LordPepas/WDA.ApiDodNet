using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Services;
using WDA.ApiDotNet.Business.Helpers;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IBooksService
    {
        Task<ResultService> CreateAsync(BooksCreateDTO booksDTO);
        Task<ResultService<PaginationResponse<BooksDTO>>> GetAsync(PageParams pageParams, string? search);
        Task<ResultService> GetByIdAsync(int id);
        Task<ResultService<List<BookRentalDTO>>> GetSummaryBooksAsync();
        Task<ResultService> UpdateAsync(BooksUpdateDTO booksDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<List<BooksCountDTO>>> GetMostRentedBooks();
    }
}
