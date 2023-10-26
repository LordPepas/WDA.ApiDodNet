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
        public async Task<ResultService> CreateAsync(BooksCreateDTO newBookDTO)
        {
            var mappedBook = _mapper.Map<Books>(newBookDTO);

            var validation = new BookCreationValidator().Validate(newBookDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var duplicateName = await _booksRepository.GetByNameAndPublisher(mappedBook.Name,mappedBook.PublisherId);
            if (duplicateName.Count > 0)
                return ResultService.BadRequest("Livro com essa editora já existente");

            var publisher = await _publishersRepository.GetById(mappedBook.PublisherId);
            if (publisher == null)
                return ResultService.NotFound("Editora não encontrada.");

            if (newBookDTO.Release > DateTime.Now.Year)
                return ResultService.BadRequest("O ano de lançamento deve ser anterior ao ano atual.");


            await _booksRepository.Create(mappedBook);

            return ResultService.Created("Livro adicionado com sucesso.");
        }


        public async Task<ResultService<BooksDTO>> GetAsync(QueryHandler queryHandler)
        {
            var result = await _booksRepository.GetAll(queryHandler);
            var mappedBooks = _mapper.Map<List<BooksDTO>>(result.Data);

            if (result.PageNumber <= 0 || result.ItemsPerpage <= 0 || result.Data.Count == 0)
                return ResultService.NotFound<BooksDTO>("Nenhum registro encontrada.");

            var paginationHeader = new PaginationHeader<BooksDTO>(
                result.PageNumber,
                result.ItemsPerpage,
                result.TotalCount,
                result.TotalPages);

            return ResultService.OKPage<BooksDTO>(mappedBooks, paginationHeader);
        }

        public async Task<ResultService<List<BooksSummaryDTO>>> GetSummaryBooksAsync()
        {
            var result = await _booksRepository.GetSummaryBooks();
            if (result.Count == 0)
                return ResultService.NotFound<List<BooksSummaryDTO>>("Nenhum registro encontrada.");

            return ResultService.Ok<List<BooksSummaryDTO>>(_mapper.Map<List<BooksSummaryDTO>>(result));
        }
        public async Task<ResultService<List<BooksAvailableDTO>>> GetSummaryAvailableBooksAsync()
        {
            var result = await _booksRepository.GetSummaryAvailableBooks();
            if (result.Count == 0)
                return ResultService.NotFound<List<BooksAvailableDTO>>("Nenhum registro encontrada.");

            return ResultService.Ok<List<BooksAvailableDTO>>(_mapper.Map<List<BooksAvailableDTO>>(result));
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var result = await _booksRepository.GetById(id);
            if (result == null)
                return ResultService.NotFound("Livro não encontrado.");

            return ResultService.Ok(_mapper.Map<BooksDTO>(result));

        }

        public async Task<ResultService<List<MostRentedBooksDTO>>> GetMostRentedBooks()
        {
            var result = await _booksRepository.MostRentedBooks();
            if (result.Count == 0)
                return ResultService.NotFound<List<MostRentedBooksDTO>>("Nenhum registro encontrada.");

            return ResultService.Ok<List<MostRentedBooksDTO>>(_mapper.Map<List<MostRentedBooksDTO>>(result));
        }

        public async Task<ResultService> UpdateAsync(BooksUpdateDTO updatedBookDTO)
        {
            var book = await _booksRepository.GetById(updatedBookDTO.Id);

            if (book == null)
                return ResultService.NotFound("Livro não encontrado.");
            if(book.Name != updatedBookDTO.Name || book.Publisher.Id != updatedBookDTO.PublisherId)
            {
            var duplicateName = await _booksRepository.GetByNameAndPublisher(updatedBookDTO.Name, updatedBookDTO.PublisherId);
            if (duplicateName.Count > 0)
                return ResultService.BadRequest("Livro com essa editora já existente");
            }

            if (updatedBookDTO.Release > DateTime.Now.Year)
                return ResultService.BadRequest("O ano de lançamento deve ser anterior ao ano atual.");


            var validation = new BookUpdateValidator().Validate(updatedBookDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);


            book = _mapper.Map(updatedBookDTO, book);
            await _booksRepository.Update(book);

            return ResultService.Ok("Livro atualizado com sucesso.");
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var book = await _booksRepository.GetById(id);
            if (book == null)
                return ResultService.NotFound("Livro não encontrado.");

            var booksAssociatedWithPublisher = await _rentalsRepository.GetByBookId(id);

            if (booksAssociatedWithPublisher.Count > 0)
                return ResultService.BadRequest("Livro está associada a aluguéis.");

            await _booksRepository.Delete(book);
            return ResultService.Ok($"Livro com id: {id} foi deletado");
        }
    }
}
