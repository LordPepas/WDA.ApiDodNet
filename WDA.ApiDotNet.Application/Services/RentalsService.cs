using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
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
        public async Task<ResultService> CreateAsync(RentalsCreateDTO newRentalDTO)
        {
            if (newRentalDTO == null)
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var validation = new RentalsCreateDTOValidator().Validate(newRentalDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var book = await _booksRepository.GetById(newRentalDTO.BookId);
            if (book == null)
                return ResultService.NotFound("Livro não encontrado!");

            var user = await _usersRepository.GetById(newRentalDTO.UserId);
            if (user == null)
                return ResultService.NotFound("Usuário não encontrado!");

            bool dateValidate = await _rentalsRepository.CheckDate(newRentalDTO.RentalDate);
            if (dateValidate == false)
                return ResultService.BadRequest("Data de aluguel não pode ser diferente da data de Hoje!");

            var bookQuantity = await _booksRepository.GetById(newRentalDTO.BookId);
            if (bookQuantity.Quantity == 0)
            {
                return ResultService.BadRequest("Livro sem estoque!");
            }


            bool? forecastValidate = await _rentalsRepository.CheckPrevisionDate(newRentalDTO.PrevisionDate, newRentalDTO.RentalDate);
            if (forecastValidate == true)
                return ResultService.BadRequest("Previsão maxima de 30 dias!");

            else if (forecastValidate == false)
                return ResultService.BadRequest("Data de Previsão não pode ser anterior à Data do Aluguel!");

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
                return ResultService.NotFound<RentalsDTO>("Nenhum registro encontrada!");

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

        public async Task<ResultService> UpdateAsync(RentalsUpdateDTO updateRentalDTO)
        {
            if (updateRentalDTO == null)
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var result = new RentalsUpdateDTOValidator().Validate(updateRentalDTO);
            if (!result.IsValid)
                return ResultService.BadRequest(result);

            var rental = await _rentalsRepository.GetById(updateRentalDTO.Id);
            if (rental == null)
                return ResultService.NotFound("Aluguel não encontrado!");

            if (rental.ReturnDate != null)
                return ResultService.BadRequest("Aluguel já devolvido!");

            bool dateValidate = await _rentalsRepository.CheckDate(updateRentalDTO.ReturnDate);
            if (dateValidate == false)
                return ResultService.BadRequest("Data de devolução não pode ser diferente da data de Hoje!");

            bool status = await _rentalsRepository.GetStatus(rental.PrevisionDate, updateRentalDTO.ReturnDate);
            if (status == true)
                rental.Status = "No prazo";
            else
                rental.Status = "Atrasado";

            rental = _mapper.Map(updateRentalDTO, rental);

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
