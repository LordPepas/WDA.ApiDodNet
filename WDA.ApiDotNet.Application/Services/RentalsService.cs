using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
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
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var result = new RentalsCreateDTOValidator().Validate(rentalDTO);
            if (!result.IsValid)
                return ResultService.BadRequest(result);

            var book = await _booksRepository.GetById(rentalDTO.BookId);
            if (book == null)
                return ResultService.NotFound("Livro não encontrado!");

            var user = await _usersRepository.GetById(rentalDTO.UserId);
            if (user == null)
                return ResultService.NotFound("Usuário não encontrado!");

            bool dateValidate = await _rentalsRepository.CheckDate(rentalDTO.RentalDate);
            if (dateValidate==false)
                return ResultService.BadRequest("Data de aluguel não pode ser diferente da data de Hoje!");

            var bookQuantity = await _booksRepository.GetById(rentalDTO.BookId);
            if (bookQuantity.Quantity == 0)
            {
                return ResultService.BadRequest("Livro sem estoque!");
            }


            bool? forecastValidate = await _rentalsRepository.CheckPrevisionDate(rentalDTO.PrevisionDate, rentalDTO.RentalDate);
            if (forecastValidate == true)
                return ResultService.BadRequest("Previsão maxima de 30 dias!");

            else if (forecastValidate == false)
                return ResultService.BadRequest("Data de Previsão não pode ser anterior à Data do Aluguel!");

            var rental = _mapper.Map<Rentals>(rentalDTO);

            rental.Status = "Pendente";
            await _rentalsRepository.Create(rental);

            return ResultService.Created("Aluguel adicionado com sucesso.");
        }

        public async Task<ResultService<RentalsDTO>> GetAsync(QueryHandler queryHandler)
        {
            var rentals = await _rentalsRepository.GetAll(queryHandler);
            var mappedRentals = _mapper.Map<List<RentalsDTO>>(rentals.Data);

            if (rentals.PageNumber <= 0 || rentals.ItemsPerpage <= 0 || rentals.Data.Count == 0)
                return ResultService.NotFound<RentalsDTO>("Nunuhm registro encontrada!");

            var paginationHeader = new PaginationHeader<RentalsDTO>(
                rentals.PageNumber,
                rentals.ItemsPerpage,
                rentals.TotalCount,
                rentals.TotalPages
            );

            var result = ResultService.OKPage<RentalsDTO>(mappedRentals, paginationHeader);

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
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var result = new RentalsUpdateDTOValidator().Validate(rentalDTO);
            if (!result.IsValid)
                return ResultService.BadRequest(result);

            var rental = await _rentalsRepository.GetById(rentalDTO.Id);
            if (rental == null)
                return ResultService.NotFound("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.BadRequest("Aluguel já devolvido!");

            bool dateValidate = await _rentalsRepository.CheckDate(rentalDTO.ReturnDate);
            if (dateValidate == false)
                return ResultService.BadRequest("Data de devolução não pode ser diferente da data de Hoje!");

            bool status = await _rentalsRepository.GetStatus(rental.PrevisionDate, rentalDTO.ReturnDate);
            if (status == true)
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
                return ResultService.NotFound("Aluguel não encontrado!");
            if (rental.Status != "Pendente")
                return ResultService.BadRequest("Aluguel já devolvido!");

            await _rentalsRepository.Delete(rental);
            return ResultService.Ok($"Aluguel com id: {id} foi deletado.");
        }
    }
}
