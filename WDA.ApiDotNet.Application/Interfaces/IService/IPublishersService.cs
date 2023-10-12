using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Services;
using WDA.ApiDotNet.Business.Helpers;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IPublishersService
    {
        Task<ResultService> CreateAsync(PublishersCreateDTO publishersDTO);
        Task<ResultService<PaginationResponse<PublishersDTO>>> GetAsync(PageParams pageParams, string? search);
        Task<ResultService> GetByIdAsync(int id);
        Task<ResultService<List<BookPublisherDTO>>> GetSummaryPublishersAsync();
        Task<ResultService> UpdateAsync(PublishersUpdateDTO publishersDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
