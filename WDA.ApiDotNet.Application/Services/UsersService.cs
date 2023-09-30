using AutoMapper;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.DTOs.Validations;
using WDA.ApiDotNet.Application.Services;
using WDA.ApiDotNet.Application.Services.Interface;
using WDA.ApiDodNet.Data.Repositories.Interface;
using WDA.ApiDotNet.Application.DTOs.PublishersDTO;

namespace WDA.ApiDodNet.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IRentalsRepository _rentalsRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IRentalsRepository rentalsRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _rentalsRepository = rentalsRepository;
            _mapper = mapper;
        }

        public async Task<ResultService> CreateAsync(UsersCreateDTO usersDTO)
        {
            if (usersDTO == null)
                return ResultService.Fail<UsersCreateDTO>("Objeto deve ser informado!");

            var result = new UsersCreateDTOValidator().Validate(usersDTO);
            if (!result.IsValid)
                return ResultService.RequestError<UsersCreateDTO>("Problemas de validação", result);

            var user = _mapper.Map<Users>(usersDTO);
            var email = await _usersRepository.GetByEmailAsync(usersDTO.Email);
            if (email.Count > 0)
            {
                return ResultService.Fail<PublishersDTO>("Email já existente");
            }
            var data = await _usersRepository.CreateAsync(user);
            return ResultService.Ok(_mapper.Map<UsersCreateDTO>(data));
        }

        public async Task<ResultService<ICollection<UsersDTO>>> GetAsync()
        {
            var users = await _usersRepository.GetByUsersAsync();
            return ResultService.Ok(_mapper.Map<ICollection<UsersDTO>>(users));
        }

        public async Task<ResultService<UsersDTO>> GetByIdAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null)
                return ResultService.Fail<UsersDTO>("Usuário não encontrado!");

            return ResultService.Ok(_mapper.Map<UsersDTO>(user));
        }



        public async Task<ResultService> UpdateAsync(UsersUpdateDTO usersDTO)
        {
            if (usersDTO == null)
                return ResultService.Fail<UsersUpdateDTO>("Objeto deve ser informado!");
            var validation = new UsersDTOValidator().Validate(usersDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<UsersUpdateDTO>("Problemas de validação", validation);
            var user = await _usersRepository.GetByIdAsync(usersDTO.Id);
            if (user == null)
                return ResultService.Fail("Usuário não encontrado!");

            user = _mapper.Map(usersDTO, user);
            await _usersRepository.UpdateAsync(user);
            return ResultService.Ok("Usuário atualizado");

        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var user = await _usersRepository.GetByIdAsync(id);
            if (user == null)
                return ResultService.Fail<UsersDTO>("Usuário não encontrado!");

            // Verifique se há usuários associados diretamente na lógica de exclusão
            var booksAssociatedWithPublisher = await _rentalsRepository.GetByUserIdAsync(id);

            if (booksAssociatedWithPublisher.Count > 0)
            {
                return ResultService.Fail<UsersDTO>("O usuário não pode ser excluída, pois está associada a aluguéis.");
            }

            await _usersRepository.DeleteAsync(user);
            return ResultService.Ok($"Usuário com id: {id} foi deletado!");
        }
    }
}
