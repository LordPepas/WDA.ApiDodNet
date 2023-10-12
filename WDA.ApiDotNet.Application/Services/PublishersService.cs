using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Models.DTOs.Validations;
using WDA.ApiDotNet.Business.Helpers;

namespace WDA.ApiDotNet.Application.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IPublishersRepository _publishersRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IMapper _mapper;

        public PublishersService(IPublishersRepository publishersRepository, IBooksRepository booksRepository, IMapper mapper)
        {
            _publishersRepository = publishersRepository;
            _booksRepository = booksRepository;
            _mapper = mapper;
        }

        public async Task<ResultService> CreateAsync(PublishersCreateDTO publishersDTO)
        {
            if (publishersDTO == null)
            {
                return ResultService.Fail("Preencha todos os campos corretamente.");
            }

            var result = new PublishersCreateDTOValidator().Validate(publishersDTO);
            if (!result.IsValid)
                return ResultService.RequestError("Problemas de validação", result);

            var publisher = _mapper.Map<Publishers>(publishersDTO);

            var name = await _publishersRepository.GetByNameAsync(publishersDTO.Name);
            if (name.Count > 0)
            {
                return ResultService.Fail("Editora já existente");
            }

            await _publishersRepository.CreateAsync(publisher);
            return ResultService.Ok("Livro adicionado com sucesso.");
        }

        public async Task<ResultService<PaginationResponse<PublishersDTO>>> GetAsync(PageParams pageParams, string search)
        {
            var publishers = await _publishersRepository.GetAllAsync(pageParams, search);

            var mappedPublishers = _mapper.Map<List<PublishersDTO>>(publishers.Data);

            var paginationHeader = new PaginationHeader<PublishersDTO>(
                publishers.CurrentPage,
                publishers.PageSize,
                publishers.TotalCount,
                publishers.TotalPages
            );

            var response = new PaginationResponse<PublishersDTO>
            {
                Header = paginationHeader,
                Data = mappedPublishers
            };

            return ResultService.Ok(response);
        }

        public async Task<ResultService<List<BookPublisherDTO>>> GetSummaryPublishersAsync()
        {
            var books = await _publishersRepository.GetSummaryPublishersAsync();
            return ResultService.Ok<List<BookPublisherDTO>>(_mapper.Map<List<BookPublisherDTO>>(books));
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var publishers = await _publishersRepository.GetByIdAsync(id);
            if (publishers == null)
                return ResultService.Fail("Editora não encontrado!");

            return ResultService.Ok(_mapper.Map<PublishersDTO>(publishers));
        }



        public async Task<ResultService> UpdateAsync(PublishersUpdateDTO publishersDTO)
        {
            if (publishersDTO == null)
                return ResultService.Fail("Objeto deve ser informado!");
            var validation = new PublishersDTOValidator().Validate(publishersDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas de validação", validation);
            var publishers = await _publishersRepository.GetByIdAsync(publishersDTO.Id);
            if (publishers == null)
                return ResultService.Fail("Editora não encontrado!");

            publishers = _mapper.Map<PublishersUpdateDTO, Publishers>(publishersDTO, publishers);
            await _publishersRepository.UpdateAsync(publishers);
            return ResultService.Ok("Livro atualizado com sucesso.");

        }
        public async Task<ResultService> DeleteAsync(int id)
        {
            var publisher = await _publishersRepository.GetByIdAsync(id);

            if (publisher == null)
            {
                return ResultService.Fail("Editora não encontrada!");
            }

            // Verifique se há livros associados diretamente na lógica de exclusão
            var booksAssociatedWithPublisher = await _booksRepository.GetByPublishersIdAsync(id);

            if (booksAssociatedWithPublisher.Count > 0)
            {
                return ResultService.Fail("A editora não pode ser excluída, pois está associada a livros.");
            }

            await _publishersRepository.DeleteAsync(publisher);

            return ResultService.Ok($"Editora com ID {id} foi excluída com sucesso.");
        }


    }
}
