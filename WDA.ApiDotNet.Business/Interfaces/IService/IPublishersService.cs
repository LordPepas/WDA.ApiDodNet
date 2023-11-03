using WDA.ApiDotNet.Business.Helpers;
using WDA.ApiDotNet.Business.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Business.Services;

namespace WDA.ApiDotNet.Business.Interfaces.IServices
{
    public interface IPublishersService
    {
        Task<ResultService> CreateAsync(PublishersCreateDTO publishersDTO);
        Task<ResultService<PublishersDTO>> GetAsync(QueryHandler queryHandler);
        Task<ResultService<List<PublishersSummaryDTO>>> GetSummaryPublishersAsync();
        Task<ResultService> GetByIdAsync(int id);
        Task<ResultService> UpdateAsync(PublishersUpdateDTO publishersDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
