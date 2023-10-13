using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;
using WDA.ApiDotNet.Application.Models.DTOs.Validations;
using WDA.ApiDotNet.Application.Services;
using WDA.ApiDotNet.Business.Helpers;

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
                return ResultService.RequestError("Problemas de validação", result);

            var user = _mapper.Map<Users>(usersDTO);
            var email = await _usersRepository.GetByEmail(usersDTO.Email);
            if (email.Count > 0)
            {
                return ResultService.Fail("Email já existente!");
            }
            await _usersRepository.Create(user);
            return ResultService.Ok("Usuário adicionado com sucesso.");
        }

        public async Task<ResultService<PaginationResponse<UsersDTO>>> GetAsync(QueryHandler queryHandler)
        {
            var users = await _usersRepository.GetAll(queryHandler);
            var mappedPublishers = _mapper.Map<List<UsersDTO>>(users.Data);

            var paginationHeader = new PaginationHeader<UsersDTO>(
                users.CurrentPage,
                users.PageSize,
                users.TotalCount,
                users.TotalPages
            );

            var response = new PaginationResponse<UsersDTO>
            {
                Header = paginationHeader,
                Data = mappedPublishers
            };

            return ResultService.Ok(response);
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
                return ResultService.Fail("Usuário não encontrado!");

            return ResultService.Ok(_mapper.Map<UsersDTO>(user));
        }

        public async Task<ResultService> UpdateAsync(UsersUpdateDTO usersDTO)
        {
            var validation = new UsersDTOValidator().Validate(usersDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas de validação", validation);
            var user = await _usersRepository.GetById(usersDTO.Id);
            if (user == null)
                return ResultService.Fail("Usuário não encontrado!");

            user = _mapper.Map(usersDTO, user);
            await _usersRepository.Update(user);
            return ResultService.Ok("Usuário atualizado.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var user = await _usersRepository.GetById(id);
            if (user == null)
                return ResultService.Fail("Usuário não encontrado!");

            var booksAssociatedWithPublisher = await _rentalsRepository.GetByUserId(id);

            if (booksAssociatedWithPublisher.Count > 0)
            {
                return ResultService.Fail("O usuário não pode ser excluída, pois está associada a aluguéis!");
            }

            await _usersRepository.Delete(user);
            return ResultService.Ok($"Usuário com id: {id} foi deletado.");
        }
    }
}
