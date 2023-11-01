using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;
using WDA.ApiDotNet.Application.Services;
using WDA.ApiDotNet.Business.Models.DTOs.Validations.CreationValidations;
using WDA.ApiDotNet.Business.Models.DTOs.Validations.UpdateValidations;

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

        public async Task<ResultService> CreateAsync(UsersCreateDTO newUserDTO)
        {
            var mappedUser = _mapper.Map<Users>(newUserDTO);

            var validation = new UserCreationValidator().Validate(newUserDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var duplicateEmail = await _usersRepository.GetByEmail(newUserDTO.Email);
            if (duplicateEmail.Count > 0)
            {
                return ResultService.BadRequest("Email já cadastrado.");
            }
            await _usersRepository.Create(mappedUser);

            return ResultService.Created("Usuário adicionado com sucesso.");
        }

        public async Task<ResultService<UsersDTO>> GetAsync(QueryHandler queryHandler)
        {
            var result = await _usersRepository.GetAll(queryHandler);

            var mappedUsers = _mapper.Map<List<UsersDTO>>(result.Data);

            if (result.PageNumber <= 0 || result.ItemsPerpage <= 0 || result.Data.Count == 0)
                return ResultService.NotFound<UsersDTO>("Nenhum registro encontrada.");

            var paginationHeader = new PaginationHeader<UsersDTO>(
                result.PageNumber,
                result.ItemsPerpage,
                result.TotalCount,
                result.TotalPages
            );

            return ResultService.OKPage<UsersDTO>(mappedUsers, paginationHeader);
        }

        public async Task<ResultService<List<UsersSummaryDTO>>> GetSummaryUsersAsync()
        {
            var result = await _usersRepository.GetSummaryUsers();
            if (result.Count == 0)
                return ResultService.NotFound<List<UsersSummaryDTO>>("Nenhum registro encontrada.");
            return ResultService.Ok<List<UsersSummaryDTO>>(_mapper.Map<List<UsersSummaryDTO>>(result));
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var result = await _usersRepository.GetById(id);
            if (result == null)
                return ResultService.NotFound("Usuário não encontrado.");

            return ResultService.Ok(_mapper.Map<UsersDTO>(result));
        }

        public async Task<ResultService> UpdateAsync(UsersUpdateDTO updatedUserDTO)
        {
            var user = await _usersRepository.GetById(updatedUserDTO.Id);
            if (user == null)
                return ResultService.NotFound("Usuário não encontrado.");


            if (user.Email != updatedUserDTO.Email)
            {
                var duplicateEmail = await _usersRepository.GetByEmail(updatedUserDTO.Email);
                if (duplicateEmail.Count > 0)
                {
                    return ResultService.BadRequest("Email já cadastrado.");
                }
            }

            var validation = new UserUpdateValidator().Validate(updatedUserDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            user = _mapper.Map(updatedUserDTO, user);
            await _usersRepository.Update(user);

            return ResultService.Ok("Usuário atualizado com sucesso.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var user = await _usersRepository.GetById(id);
            if (user == null)
                return ResultService.NotFound("Usuário não encontrado.");

            var booksAssociatedWithPublisher = await _rentalsRepository.GetByUserId(id);

            if (booksAssociatedWithPublisher.Count > 0)
            {
                return ResultService.BadRequest("Usuário está associada a aluguéis.");
            }

            await _usersRepository.Delete(user);
            return ResultService.Ok($"Usuário com id: {id} foi deletado.");
        }
    }
}
