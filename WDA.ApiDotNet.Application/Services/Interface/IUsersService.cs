using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDotNet.Application.Services.Interface
{
    public interface IUsersService
    {
        //Retorna a coleção de objetos UsersDTO
        Task<ResultService<ICollection<UsersDTO>>> GetAsync(PageParams pageParams, string? value);
        Task<ResultService<UsersDTO>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(UsersCreateDTO usersDTO);
        Task<ResultService> UpdateAsync(UsersUpdateDTO usersDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
