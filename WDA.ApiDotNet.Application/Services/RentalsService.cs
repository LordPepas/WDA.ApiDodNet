using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Models.DTOs.Validations;

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
        public async Task<ResultService> CreateAsync(RentalsCreateDTO rentalDTO)
        {
            if (rentalDTO == null)
                return ResultService.Fail("Objeto deve ser informado corretamente!");

            var result = new RentalsCreateDTOValidator().Validate(rentalDTO);
            if (!result.IsValid)
                return ResultService.RequestError("Problemas de validação", result);

            var book = await _booksRepository.GetById(rentalDTO.BookId);
            if (book == null)
                return ResultService.Fail("Livro não encontrado!");

            var user = await _usersRepository.GetById(rentalDTO.UserId);
            if (user == null)
                return ResultService.Fail("Usuário não encontrado!");

            bool dateValidate = await _rentalsRepository.CheckDate(rentalDTO.RentalDate);
            if (dateValidate)
                return ResultService.Fail("Data de aluguel não pode ser diferente da data de Hoje!");

            bool? forecastValidate = await _rentalsRepository.CheckPrevisionDate(rentalDTO.PrevisionDate, rentalDTO.RentalDate);
            if (forecastValidate == true)
                return ResultService.Fail("Previsão maxima de 30 dias!");
            else if (forecastValidate == false)
                return ResultService.Fail("Data de Previsão não pode ser anterior à Data do Aluguel!");

            var rental = _mapper.Map<Rentals>(rentalDTO);

            rental.Status = "Pendente";
            await _rentalsRepository.Create(rental);

            return ResultService.Ok("Aluguel adicionado com sucesso.");
        }

        public async Task<ResultService<RentalsDTO>> GetAsync(QueryHandler queryHandler)
        {
            var rentals = await _rentalsRepository.GetAll(queryHandler);
            var mappedRentals = _mapper.Map<List<RentalsDTO>>(rentals.Data);

            var paginationHeader = new PaginationHeader<RentalsDTO>(
                rentals.CurrentPage,
                rentals.PageSize,
                rentals.TotalCount,
                rentals.TotalPages
            );

            CustomHeaders<RentalsDTO> customHeaders = null;

            if (!string.IsNullOrWhiteSpace(queryHandler.Filter.OrderBy) || !string.IsNullOrWhiteSpace(queryHandler.Filter.SearchValue))
            {
                customHeaders = new CustomHeaders<RentalsDTO>(
                    queryHandler.Filter.OrderBy,
                    queryHandler.Filter.SearchValue
                );
            }

            var result = ResultService.OKPage<RentalsDTO>(paginationHeader, mappedRentals, customHeaders);

            return result;
        }

        public async Task<ResultService<RentalsDTO>> GetByIdAsync(int id)
        {
            var rental = await _rentalsRepository.GetById(id);

            return ResultService.Ok(_mapper.Map<RentalsDTO>(rental));
        }

        public async Task<ResultService> UpdateAsync(RentalsUpdateDTO rentalDTO)
        {
            if (rentalDTO == null)
                return ResultService.Fail("Objeto deve ser informado corretamente!");

            var result = new RentalsUpdateDTOValidator().Validate(rentalDTO);
            if (!result.IsValid)
                return ResultService.RequestError("Problemas de validação", result);

            var rental = await _rentalsRepository.GetById(rentalDTO.Id);
            if (rental == null)
                return ResultService.Fail("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.Fail("Aluguel já devolvido!");

            bool dateValidate = await _rentalsRepository.CheckDate(rentalDTO.ReturnDate);
            if (dateValidate == true)
                return ResultService.Fail("Data de devolução não pode ser diferente da data de Hoje!");

            bool status = await _rentalsRepository.GetStatus(rental.PrevisionDate, rentalDTO.ReturnDate);
            if (status)
                rental.Status = "No prazo";
            else
                rental.Status = "Atrasado";

            rental = _mapper.Map(rentalDTO, rental);

            await _rentalsRepository.Update(rental);

            return ResultService.Ok("Aluguel atualizado com sucesso.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var rental = await _rentalsRepository.GetById(id);
            if (rental == null)
                return ResultService.Fail("Aluguel não encontrado!");
            if (rental.Status != "Pendente")
                return ResultService.Fail("Aluguel já devolvido!");

            await _rentalsRepository.Delete(rental);
            return ResultService.Ok($"Aluguel com id: {id} foi deletado.");
        }
    }
}
