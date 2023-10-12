using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Models.DTOs.Validations;
using WDA.ApiDotNet.Business.Helpers;

namespace WDA.ApiDotNet.Application.Services
{
    public class RentalsService : IRentalsService
    {
        private readonly IRentalsRepository _rentalsRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public RentalsService(IRentalsRepository rentalsRepository, IBooksRepository booksRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _rentalsRepository = rentalsRepository;
            _booksRepository = booksRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }
        public async Task<ResultService> CreateAsync(RentalsCreateDTO rentalsDTO)
        {
            var result = new RentalsCreateDTOValidator().Validate(rentalsDTO);
            if (!result.IsValid)
                return ResultService.RequestError("Problemas de validação", result);

            var book = await _booksRepository.GetByIdAsync(rentalsDTO.BookId);
            if (book == null)
                return ResultService.Fail("Livro não encontrado!");

            var user = await _usersRepository.GetByIdAsync(rentalsDTO.UserId);
            if (user == null)
                return ResultService.Fail("Usuário não encontrado!");

            bool dateValidate = await _rentalsRepository.CheckDate(rentalsDTO.RentalDate);
            if (dateValidate)
                return ResultService.Fail("Data de aluguel não pode ser diferente da data de Hoje!");

            bool? forecastValidate = await _rentalsRepository.CheckPrevisionDate(rentalsDTO.PrevisionDate, rentalsDTO.RentalDate);
            if (forecastValidate == true)
                return ResultService.Fail("Previsão maxima de 30 dias!");
            else if (forecastValidate == false)
                return ResultService.Fail("Data de Previsão não pode ser anterior à Data do Aluguel!");

            var rental = _mapper.Map<Rentals>(rentalsDTO);

            rental.Status = "Pendente";
            await _rentalsRepository.CreateAsync(rental);

            return ResultService.Ok("Aluguel adicionado com sucesso.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var rental = await _rentalsRepository.GetByIdAsync(id);
            if (rental == null)
                return ResultService.Fail("Aluguel não encontrado!");
            if (rental.Status != "Pendente")
                return ResultService.Fail("Aluguel já devolvido!");

            await _rentalsRepository.DeleteAsync(rental);
            return ResultService.Ok($"Aluguel com id: {id} foi deletado");
        }
        public async Task<ResultService<PaginationResponse<RentalsDTO>>> GetAsync(PageParams pageParams, string? search)
        {
            var rentals = await _rentalsRepository.GetAllAsync(pageParams, search);
            var mappedRentals = _mapper.Map<List<RentalsDTO>>(rentals.Data);

            var paginationHeader = new PaginationHeader<RentalsDTO>(
                rentals.CurrentPage,
                rentals.PageSize,
                rentals.TotalCount,
                rentals.TotalPages
            );

            var response = new PaginationResponse<RentalsDTO>
            {
                Header = paginationHeader,
                Data = mappedRentals
            };

            return ResultService.Ok(response);
        }

        public async Task<ResultService<RentalsDTO>> GetByIdAsync(int id)
        {
            var rental = await _rentalsRepository.GetByIdAsync(id);

            return ResultService.Ok(_mapper.Map<RentalsDTO>(rental));
        }

        public async Task<ResultService> UpdateAsync(RentalsUpdateDTO rentalsDTO)
        {
            var validation = new RentalsUpdateDTOValidator().Validate(rentalsDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas de validação", validation);
            var rental = await _rentalsRepository.GetByIdAsync(rentalsDTO.Id);
            if (rental.ReturnDate != null)
                return ResultService.Fail("Aluguel já devolvido!");

            bool dateValidate = await _rentalsRepository.CheckDate(rentalsDTO.ReturnDate);
            if (dateValidate)
                return ResultService.Fail("Data de devolução não pode ser diferente da data de Hoje!");

            bool status = await _rentalsRepository.GetStatus(rental.PrevisionDate, rentalsDTO.ReturnDate);
            if (status)
                rental.Status = "No prazo";
            else
                rental.Status = "Atrasado";

            await _rentalsRepository.UpdateAsync(rental);

            return ResultService.Ok<RentalsDTO>(_mapper.Map<RentalsDTO>(rental));
        }
    }
}
