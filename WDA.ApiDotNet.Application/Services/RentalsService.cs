using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
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
        public async Task<ResultService> CreateAsync(RentalsCreateDTO rentalsDTO)
        {
            if (rentalsDTO == null)
                return ResultService.Fail<RentalsCreateDTO>("Objeto deve ser informado!");

            var result = new RentalsCreateDTOValidator().Validate(rentalsDTO);
            if (!result.IsValid)
                return ResultService.RequestError<RentalsCreateDTO>("Problemas de validação", result);

            var book = await _booksRepository.GetByIdAsync(rentalsDTO.BookId);
            if (book == null)
                return ResultService.Fail<RentalsCreateDTO>("Livro não encontrado!");

            var user = await _usersRepository.GetByIdAsync(rentalsDTO.UserId);
            if (user == null)
                return ResultService.Fail<RentalsCreateDTO>("Usuário não encontrado!");

            bool dateValidate = await _rentalsRepository.CheckDate(rentalsDTO.RentalDate);
            if (dateValidate)
                return ResultService.Fail<RentalsCreateDTO>("Data de aluguel não pode ser diferente da data de Hoje!");

            bool? forecastValidate = await _rentalsRepository.CheckPrevisionDate(rentalsDTO.PrevisionDate, rentalsDTO.RentalDate);
            if (forecastValidate == true)
                return ResultService.Fail<RentalsCreateDTO>("Previsão maxima de 30 dias!");
            else if (forecastValidate == false)
                return ResultService.Fail<RentalsCreateDTO>("Data de Previsão não pode ser anterior à Data do Aluguel!");

            var rental = _mapper.Map<Rentals>(rentalsDTO);

            rental.Status = "Pendente";

            var data = await _rentalsRepository.CreateAsync(rental);

            return ResultService.Ok<RentalsDTO>(_mapper.Map<RentalsDTO>(data));
        }


        public async Task<ResultService> DeleteAsync(int id)
        {
            var rental = await _rentalsRepository.GetByIdAsync(id);
            if (rental == null)
                return ResultService.Fail<RentalsDTO>("Aluguel não encontrado!");

            await _rentalsRepository.DeleteAsync(rental);
            return ResultService.Ok($"Aluguel com id: {id} foi deletado");
        }
        public async Task<ResultService<ICollection<RentalsDTO>>> GetAsync(PageParams pageParams, string? search)
        {
            var rentals = await _rentalsRepository.GetAllAsync(pageParams, search);
            return ResultService.Ok<ICollection<RentalsDTO>>(_mapper.Map<ICollection<RentalsDTO>>(rentals));
        }

        public async Task<ResultService<RentalsDTO>> GetByIdAsync(int id)
        {
            var rental = await _rentalsRepository.GetByIdAsync(id);
            if (rental == null)
                return ResultService.Fail<RentalsDTO>("Aluguel não encontrado!");

            return ResultService.Ok(_mapper.Map<RentalsDTO>(rental));

        }

        public async Task<ResultService> UpdateAsync(RentalsUpdateDTO rentalsDTO)
        {

            if (rentalsDTO == null)
                return ResultService.Fail<RentalsUpdateDTO>("Objeto deve ser informado!");
            var validation = new RentalsUpdateDTOValidator().Validate(rentalsDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<RentalsUpdateDTO>("Problemas de validação", validation);
            var rental = await _rentalsRepository.GetByIdAsync(rentalsDTO.Id);
            if (rental.ReturnDate != null)
                return ResultService.Fail<RentalsUpdateDTO>("Aluguel já devolvido!");

            if (rental == null)
                return ResultService.Fail<RentalsUpdateDTO>("Aluguel não encontrado!");

            rental = _mapper.Map<RentalsUpdateDTO, Rentals>(rentalsDTO, rental);
            var book = await _booksRepository.GetByIdAsync(rental.BookId);
            if (book == null)
                return ResultService.Fail<RentalsUpdateDTO>("Livro não encontrado!");


            bool dateValidate = await _rentalsRepository.CheckDate(rentalsDTO.ReturnDate);
            if (dateValidate)
                return ResultService.Fail<RentalsUpdateDTO>("Data de devolução não pode ser diferente da data de Hoje!");

            var user = await _usersRepository.GetByIdAsync(rental.UserId);
            if (user == null)
                return ResultService.Fail<RentalsUpdateDTO>("Usuário não encontrado!");

            bool status = await _rentalsRepository.GetStatus(rental.PrevisionDate, rentalsDTO.ReturnDate);
            if (status)
                rental.Status = "No prazo";
            else
                rental.Status = "Atrasado";

            await _rentalsRepository.UpdateAsync(rental);

            return ResultService.Ok<RentalsDTO>(_mapper.Map<RentalsDTO>(rental));
        }
        public async Task<ResultService<List<BookRentalDTO>>> GetSelectBooksAsync()
        {
            var books = await _rentalsRepository.GetSelectBooksAsync();
            return ResultService.Ok<List<BookRentalDTO>>(_mapper.Map<List<BookRentalDTO>>(books));
        }
        public async Task<ResultService<List<UserRentalDTO>>> GetSelectUsersAsync()
        {
            var books = await _rentalsRepository.GetSelectUsersAsync();
            return ResultService.Ok<List<UserRentalDTO>>(_mapper.Map<List<UserRentalDTO>>(books));
        }
    }
}
