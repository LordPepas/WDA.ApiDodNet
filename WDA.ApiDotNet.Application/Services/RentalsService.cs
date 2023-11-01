using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Business.Models.DTOs.Validations.CreationValidations;

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
        public async Task<ResultService> CreateAsync(RentalsCreateDTO newRentalDTO)
        {
            var validation = new RentalCreationValidator().Validate(newRentalDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var book = await _booksRepository.GetById(newRentalDTO.BookId);
            if (book == null)
                return ResultService.NotFound("Livro não encontrado.");

            var user = await _usersRepository.GetById(newRentalDTO.UserId);
            if (user == null)
                return ResultService.NotFound("Usuário não encontrado.");

            var userRental = await _rentalsRepository.GetRentalByUserIdandBookId(book.Id, user.Id);
            if (userRental.Count > 0)
                return ResultService.BadRequest("Usuário já possui aluguel desse livro.");

            var bookQuantity = await _booksRepository.GetById(newRentalDTO.BookId);
            if (bookQuantity.Quantity == 0)
            {
                return ResultService.BadRequest("Livro sem estoque.");
            }


            DateTime rentalDate = DateTime.Now.Date;

            DateTime previsionDate = newRentalDTO.PrevisionDate.Value.Date;

            if ((previsionDate - rentalDate).Days > 30)
            {
                return ResultService.BadRequest("A data de previsão deve ser no máximo 30 dias após a data de aluguel.");
            }

            var rental = _mapper.Map<Rentals>(newRentalDTO);

            rental.Status = "Pendente";
            await _rentalsRepository.Create(rental);

            return ResultService.Created("Aluguel adicionado com sucesso.");
        }

        public async Task<ResultService<RentalsDTO>> GetAsync(QueryHandler queryHandler)
        {
            var result = await _rentalsRepository.GetAll(queryHandler);
            var mappedRentals = _mapper.Map<List<RentalsDTO>>(result.Data);

            if (result.PageNumber <= 0 || result.ItemsPerpage <= 0 || result.Data.Count == 0)
                return ResultService.NotFound<RentalsDTO>("Nenhum registro encontrada.");

            var paginationHeader = new PaginationHeader<RentalsDTO>(
                result.PageNumber,
                result.ItemsPerpage,
                result.TotalCount,
                result.TotalPages
            );

            return ResultService.OKPage<RentalsDTO>(mappedRentals, paginationHeader);
        }

        public async Task<ResultService<RentalsDTO>> GetByIdAsync(int id)
        {
            var result = await _rentalsRepository.GetById(id);

            return ResultService.Ok(_mapper.Map<RentalsDTO>(result));
        }

        public async Task<ResultService> UpdateAsync(int id)
        {
            var rental = await _rentalsRepository.GetById(id);
            if (rental == null)
                return ResultService.NotFound("Aluguel não encontrado.");

            if (rental.ReturnDate != null)
                return ResultService.BadRequest("Aluguel já devolvido.");

            if (rental.PrevisionDate.Date >= DateTime.Now.Date)
                rental.Status = "No prazo";
            else
                rental.Status = "Atrasado";

            rental.ReturnDate = DateTime.Now.Date;

            await _rentalsRepository.Update(rental);

            return ResultService.Ok("Aluguel devolvido com sucesso.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var rental = await _rentalsRepository.GetById(id);
            if (rental == null)
                return ResultService.NotFound("Aluguel não encontrado.");

            if (rental.Status != "Pendente")
                return ResultService.BadRequest("Aluguel já devolvido.");

            await _rentalsRepository.Delete(rental);

            return ResultService.Ok($"Aluguel com id: {id} foi deletado.");
        }
    }
}
