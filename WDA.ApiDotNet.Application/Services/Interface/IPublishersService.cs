using WDA.ApiDotNet.Application.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDotNet.Application.Services.Interface
{
    public interface IPublishersService
    {
        Task<ResultService<ICollection<PublishersDTO>>> GetAsync(PageParams pageParams, string? search);
        Task<ResultService<PublishersDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(PublishersCreateDTO publishersDTO);
        Task<ResultService> UpdateAsync(PublishersUpdateDTO publishersDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
