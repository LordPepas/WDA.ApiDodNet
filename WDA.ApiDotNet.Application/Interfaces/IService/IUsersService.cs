using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;
using WDA.ApiDotNet.Application.Services;

namespace WDA.ApiDotNet.Application.Interfaces.IServices
{
    public interface IUsersService
    {
        //Retorna a coleção de objetos UsersDTO
        Task<ResultService<ICollection<UsersDTO>>> GetAsync(PageParams pageParams, string? search);
        Task<ResultService<UsersDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(UsersCreateDTO usersDTO);
        Task<ResultService> UpdateAsync(UsersUpdateDTO usersDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
