using WDA.ApiDotNet.Application.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Services;

namespace WDA.ApiDotNet.Application.Services.Interface
{
    public interface IPublishersService
    {
        //Retorna a coleção de objetos PublishersDTO
        Task<ResultService<ICollection<PublishersDTO>>> GetAsync();
        Task<ResultService<PublishersDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(PublishersCreateDTO publishersDTO);
        Task<ResultService> UpdateAsync(PublishersUpdateDTO publishersDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
