using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;
using WDA.ApiDotNet.Application.Models.DTOs.Validations;
using WDA.ApiDotNet.Application.Services;

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

            var result = new UsersCreateDTOValidator().Validate(usersDTO);
            if (!result.IsValid)
                return ResultService.BadRequest(result);

            var user = _mapper.Map<Users>(usersDTO);
            var email = await _usersRepository.GetByEmail(usersDTO.Email);
            if (email.Count > 0)
            {
                return ResultService.BadRequest("Email já existente!");
            }
            await _usersRepository.Create(user);

            return ResultService.Created("Usuário adicionado com sucesso.");
        }

        public async Task<ResultService<UsersDTO>> GetAsync(QueryHandler queryHandler)
        {
            var users = await _usersRepository.GetAll(queryHandler);
            var mappedUsers = _mapper.Map<List<UsersDTO>>(users.Data);

            if (users.PageNumber <= 0 || users.ItemsPerpage <= 0 || users.Data.Count == 0)
                return ResultService.NotFound<UsersDTO>("Nunuhm registro encontrada!");

            var paginationHeader = new PaginationHeader<UsersDTO>(
                users.PageNumber,
                users.ItemsPerpage,
                users.TotalCount,
                users.TotalPages
            );

            var result = ResultService.OKPage<UsersDTO>(mappedUsers, paginationHeader);

            return result;
        }

        public async Task<ResultService<List<UsersSummaryDTO>>> GetSummaryUsersAsync()
        {
            var books = await _usersRepository.GetSummaryUsers();
            return ResultService.Ok<List<UsersSummaryDTO>>(_mapper.Map<List<UsersSummaryDTO>>(books));
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var user = await _usersRepository.GetById(id);
            if (user == null)
                return ResultService.NotFound("Usuário não encontrado!");

            return ResultService.Ok(_mapper.Map<UsersDTO>(user));
        }

        public async Task<ResultService> UpdateAsync(UsersUpdateDTO usersDTO)
        {
            var validation = new UsersDTOValidator().Validate(usersDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);
            var user = await _usersRepository.GetById(usersDTO.Id);
            if (user == null)
                return ResultService.NotFound("Usuário não encontrado!");

            user = _mapper.Map(usersDTO, user);
            await _usersRepository.Update(user);
            return ResultService.Ok("Usuário atualizado.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var user = await _usersRepository.GetById(id);
            if (user == null)
                return ResultService.NotFound("Usuário não encontrado!");

            var booksAssociatedWithPublisher = await _rentalsRepository.GetByUserId(id);

            if (booksAssociatedWithPublisher.Count > 0)
            {
                return ResultService.BadRequest("O usuário não pode ser excluída, pois está associada a aluguéis!");
            }

            await _usersRepository.Delete(user);
            return ResultService.Ok($"Usuário com id: {id} foi deletado.");
        }
    }
}
