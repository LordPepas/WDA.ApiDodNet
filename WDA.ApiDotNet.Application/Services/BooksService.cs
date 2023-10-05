using AutoMapper;
using WDA.ApiDodNet.Application.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.DTOs.Validations;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Services.Interface;

namespace WDA.ApiDotNet.Application.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IPublishersRepository _publishersRepository;
        private readonly IRentalsRepository _rentalsRepository;
        private readonly IMapper _mapper;

        public BooksService(IBooksRepository booksRepository, IPublishersRepository publishersRepository, IRentalsRepository rentalsRepository, IMapper mapper)
        {
            _booksRepository = booksRepository;
            _publishersRepository = publishersRepository;
            _rentalsRepository = rentalsRepository;
            _mapper = mapper;
        }
        public async Task<ResultService> CreateAsync(BooksCreateDTO booksDTO)
        {
            if (booksDTO == null)
                return ResultService.Fail<BooksCreateDTO>("Objeto deve ser informado!");

            var result = new BooksCreateDTOValidator().Validate(booksDTO);
            if (!result.IsValid)
                return ResultService.RequestError<BooksCreateDTO>("Problemas de validação", result);



            var book = _mapper.Map<Books>(booksDTO);
            var duplicateName = await _booksRepository.GetByNameAsync(booksDTO.Name);
            if (duplicateName.Count > 0)
            {
                return ResultService.Fail<PublishersDTO>("Livro já existente");
            }
            var publisher = await _publishersRepository.GetByIdAsync(booksDTO.PublisherId);
            if (publisher == null)
                return ResultService.Fail<BooksCreateDTO>("Editora não encontrada!");

            var data = await _booksRepository.CreateAsync(book);
            return ResultService.Ok<BooksCreateDTO>(_mapper.Map<BooksCreateDTO>(data));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var book = await _booksRepository.GetByIdAsync(id);
            if (book == null)
                return ResultService.Fail<BooksDTO>("Livro não encontrado!");
            var booksAssociatedWithPublisher = await _rentalsRepository.GetByBookIdAsync(id);

            if (booksAssociatedWithPublisher.Count > 0)
            {
                return ResultService.Fail<PublishersDTO>("A Livro não pode ser excluída, pois está associada a aluguéis.");
            }

            await _booksRepository.DeleteAsync(book);
            return ResultService.Ok($"Livro com id: {id} foi deletado");
        }

        public async Task<ResultService<List<BooksDTO>>> GetAsync(PageParams pageParams, string? value)
        {
            var books = await _booksRepository.GetAllAsync(pageParams, value);
            return ResultService.Ok<List<BooksDTO>>(_mapper.Map<List<BooksDTO>>(books));
        }

        public async Task<ResultService<BooksDTO>> GetByIdAsync(int id)
        {
            var book = await _booksRepository.GetByIdAsync(id);
            if (book == null)
                return ResultService.Fail<BooksDTO>("Livro não encontrado!");

            return ResultService.Ok(_mapper.Map<BooksDTO>(book));

        }

        public async Task<ResultService> UpdateAsync(BooksUpdateDTO booksDTO)
        {
            if (booksDTO == null)
                return ResultService.Fail<BooksUpdateDTO>("Objeto deve ser informado!");
            var validation = new BooksDTOValidator().Validate(booksDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<BooksUpdateDTO>("Problemas de validação", validation);
            var book = await _booksRepository.GetByIdAsync(booksDTO.Id);
            if (book == null)
                return ResultService.Fail<BooksUpdateDTO>("Livro não encontrado!");

            book = _mapper.Map<BooksUpdateDTO, Books>(booksDTO, book);
            await _booksRepository.UpdateAsync(book);
            return ResultService.Ok("Livro Atualizado!");
        }
    }
}
