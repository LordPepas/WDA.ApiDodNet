using AutoMapper;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.DTOs.Validations;
using WDA.ApiDotNet.Application.Services.Interface;
using WDA.ApiDodNet.Data.Repositories.Interface;
using WDA.ApiDotNet.Application.DTOs.RentalsDTO;

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

            //var delayedRental = await _rentalsRepository.CheckDelayAsync(user.Id);

            //if (delayedRental != null)
            //    return ResultService.Fail<RentalsCreateDTO>("Usuário com aluguel em pedente!");

            var rental = _mapper.Map<Rentals>(rentalsDTO);

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
        public async Task<ResultService<ICollection<RentalsDTO>>> GetAsync()
        {
            var rentals = await _rentalsRepository.GetByRentalsAsync();
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
            if (rental == null)
                return ResultService.Fail<RentalsUpdateDTO>("Aluguel não encontrado!");

            rental = _mapper.Map<RentalsUpdateDTO, Rentals>(rentalsDTO, rental);
            var book = await _booksRepository.GetByIdAsync(rental.BookId);
            if (book == null)
                return ResultService.Fail<RentalsUpdateDTO>("Livro não encontrado!");

            var user = await _usersRepository.GetByIdAsync(rental.UserId);
            if (user == null)
                return ResultService.Fail<RentalsUpdateDTO>("Usuário não encontrado!");

            await _rentalsRepository.UpdateAsync(rental);

            return ResultService.Ok<RentalsDTO>(_mapper.Map<RentalsDTO>(rental));
        }
    }
}
