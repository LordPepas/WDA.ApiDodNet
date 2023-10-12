using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Models.DTOs.Validations;
using WDA.ApiDotNet.Business.Helpers;

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
            {
                return ResultService.Fail("Preencha todos os campos corretamente.");
            }
            var validation = new BooksCreateDTOValidator().Validate(booksDTO);
            if (!validation.IsValid)
            {
                return ResultService.RequestError("Problemas de validação", validation);
            }
            var book = _mapper.Map<Books>(booksDTO);
            var duplicateName = await _booksRepository.GetByNameAsync(book.Name);
            if (duplicateName.Count > 0)
            {
                return ResultService.Fail("Livro já existente");
            }
            var publisher = await _publishersRepository.GetByIdAsync(book.PublisherId);
            if (publisher == null)
                return ResultService.Fail("Editora não encontrada!");

            await _booksRepository.CreateAsync(book);
            return ResultService.Ok("Livro adicionado com sucesso.");
        }



        public async Task<ResultService<PaginationResponse<BooksDTO>>> GetAsync(PageParams pageParams, string? search)
        {
            var books = await _booksRepository.GetAllAsync(pageParams, search);
            var mappedBooks = _mapper.Map<List<BooksDTO>>(books.Data);

            var paginationHeader = new PaginationHeader<BooksDTO>(books.CurrentPage, books.PageSize, books.TotalCount, books.TotalPages);

            var response = new PaginationResponse<BooksDTO>
            {
                Header = paginationHeader,
                Data = mappedBooks
            };

            return ResultService.Ok(response);
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var book = await _booksRepository.GetByIdAsync(id);
            if (book == null)
                return ResultService.Fail("Livro não encontrado!");

            return ResultService.Ok(_mapper.Map<BooksDTO>(book));

        }

        public async Task<ResultService> UpdateAsync(BooksUpdateDTO booksDTO)
        {
            if (booksDTO == null)
            {
                return ResultService.Fail("Preencha todos os campos corretamente.");
            }

            var validation = new BooksDTOValidator().Validate(booksDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas de validação", validation);
            var book = _mapper.Map<Books>(booksDTO);
            var bookExists = await _booksRepository.GetByIdAsync(book.Id);
            if (bookExists == null)
                return ResultService.Fail("Livro não encontrado!");

            await _booksRepository.UpdateAsync(book);

            return ResultService.Ok("Livro Atualizado!");
        }
        public async Task<ResultService<List<BookRentalDTO>>> GetSummaryBooksAsync()
        {
            var books = await _booksRepository.GetSummaryBooksAsync();
            return ResultService.Ok<List<BookRentalDTO>>(_mapper.Map<List<BookRentalDTO>>(books));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var book = await _booksRepository.GetByIdAsync(id);
            if (book == null)
                return ResultService.Fail("Livro não encontrado!");
            var booksAssociatedWithPublisher = await _rentalsRepository.GetByBookIdAsync(id);

            if (booksAssociatedWithPublisher.Count > 0)
            {
                return ResultService.Fail("A Livro não pode ser excluída, pois está associada a aluguéis.");
            }

            await _booksRepository.DeleteAsync(book);
            return ResultService.Ok($"Livro com id: {id} foi deletado");
        }
        public async Task<ResultService<List<BooksCountDTO>>> GetMostRentedBooks()
        {
            var totalCount = await _booksRepository.MostRentedBooks();

            return ResultService.Ok<List<BooksCountDTO>>(_mapper.Map<List<BooksCountDTO>>(totalCount));
        }
    }
}
