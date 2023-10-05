using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDotNet.Application.Services.Interface
{
    public interface IBooksService
    {
        Task<ResultService<List<BooksDTO>>> GetAsync(PageParams pageParams, string? value);
        Task<ResultService<BooksDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(BooksCreateDTO booksDTO);
        Task<ResultService> UpdateAsync(BooksUpdateDTO booksDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
