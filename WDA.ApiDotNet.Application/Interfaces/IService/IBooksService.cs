using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Services;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IBooksService
    {
        Task<ResultService<List<BooksDTO>>> GetAsync(PageParams pageParams, string? search);
        Task<ResultService<BooksDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(BooksCreateDTO booksDTO);
        Task<ResultService> UpdateAsync(BooksUpdateDTO booksDTO);
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<List<BooksCountDTO>>> GetMostRentedBooks();
        Task<ResultService<List<BookPublisherDTO>>> GetSelectPublishersAsync();
    }
}
