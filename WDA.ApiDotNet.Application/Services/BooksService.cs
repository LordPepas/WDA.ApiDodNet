#pragma warning disable CS8600

using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.Validations;

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
        public async Task<ResultService> CreateAsync(BooksCreateDTO bookDTO)
        {
            if (bookDTO == null)
                return ResultService.BadRequest("Preencha todos os campos corretamente.");

            var mappedBook = _mapper.Map<Books>(bookDTO);

            var validation = new BooksCreateDTOValidator().Validate(bookDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var duplicateName = await _booksRepository.GetByName(mappedBook.Name);
            if (duplicateName.Count > 0)
                return ResultService.BadRequest("Livro já existente");

            if (bookDTO.Release > DateTime.Now.Year)
                return ResultService.BadRequest("O ano de lançamento deve ser anterior ao ano atual.");

            var publisher = await _publishersRepository.GetById(mappedBook.PublisherId);
            if (publisher == null)
                return ResultService.NotFound("Editora não encontrada!");

            await _booksRepository.Create(mappedBook);

            return ResultService.Created("Livro adicionado com sucesso.");
        }

        public async Task<ResultService<BooksDTO>> GetAsync(QueryHandler queryHandler)
        {
            var books = await _booksRepository.GetAll(queryHandler);
            var mappedBooks = _mapper.Map<List<BooksDTO>>(books.Data);

            var paginationHeader = new PaginationHeader<BooksDTO>(books.PageNumber, books.ItemsPerpage, books.TotalCount, books.TotalPages);

            return ResultService.OKPage<BooksDTO>(mappedBooks, paginationHeader);
        }

        public async Task<ResultService<List<BooksSummaryDTO>>> GetSummaryBooksAsync()
        {
            var books = await _booksRepository.GetSummaryBooks();
            return ResultService.Ok<List<BooksSummaryDTO>>(_mapper.Map<List<BooksSummaryDTO>>(books));
        }
        public async Task<ResultService<List<BooksAvailableDTO>>> GetSummaryAvailableBooksAsync()
        {
            var books = await _booksRepository.GetSummaryAvailableBooks();
            return ResultService.Ok<List<BooksAvailableDTO>>(_mapper.Map<List<BooksAvailableDTO>>(books));
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var book = await _booksRepository.GetById(id);
            if (book == null)
                return ResultService.NotFound("Livro não encontrado!");

            return ResultService.Ok(_mapper.Map<BooksDTO>(book));

        }

        public async Task<ResultService<List<MostRentedBooksDTO>>> GetMostRentedBooks()
        {
            var books = await _booksRepository.MostRentedBooks();

            return ResultService.Ok<List<MostRentedBooksDTO>>(_mapper.Map<List<MostRentedBooksDTO>>(books));
        }

        public async Task<ResultService> UpdateAsync(BooksUpdateDTO bookDTO)
        {
            if (bookDTO == null)
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var validation = new BooksDTOValidator().Validate(bookDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);
            var book = await _booksRepository.GetById(bookDTO.Id);
            if (book == null)
                return ResultService.NotFound("Livro não encontrado!");

            book = _mapper.Map(bookDTO, book);
            await _booksRepository.Update(book);
            return ResultService.Ok("Livro atualizado com sucesso.");

        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var book = await _booksRepository.GetById(id);
            if (book == null)
                return ResultService.NotFound("Livro não encontrado!");

            var booksAssociatedWithPublisher = await _rentalsRepository.GetByBookId(id);

            if (booksAssociatedWithPublisher.Count > 0)
                return ResultService.BadRequest("A Livro não pode ser excluída, pois está associada a aluguéis.");

            await _booksRepository.Delete(book);
            return ResultService.Ok($"Livro com id: {id} foi deletado");
        }
    }
}
