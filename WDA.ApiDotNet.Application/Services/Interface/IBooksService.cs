using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.DTOs.BooksDTO;

namespace WDA.ApiDotNet.Application.Services.Interface
{
    public interface IBooksService
    {
        Task<ResultService<ICollection<BooksDTO>>> GetAsync();
        Task<ResultService<BooksDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(BooksCreateDTO booksDTO);
        Task<ResultService> UpdateAsync(BooksUpdateDTO booksDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
