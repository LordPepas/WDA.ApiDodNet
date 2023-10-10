using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Services;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
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
