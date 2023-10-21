#pragma warning disable CS8600

using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Models.DTOs.Validations;

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

        public async Task<ResultService> CreateAsync(PublishersCreateDTO publisherDTO)
        {
            if (publisherDTO == null)
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var mappedPublisher = _mapper.Map<Publishers>(publisherDTO);

            var result = new PublishersCreateDTOValidator().Validate(publisherDTO);
            if (!result.IsValid)
                return ResultService.BadRequest(result);

            var name = await _publishersRepository.GetByName(publisherDTO.Name);
            if (name.Count > 0)
                return ResultService.BadRequest("Editora já existente!");

            await _publishersRepository.Create(mappedPublisher);
            return ResultService.Created("Editora adicionado com sucesso.");
        }

        public async Task<ResultService<PublishersDTO>> GetAsync(QueryHandler queryHandler)
        {
            var publishers = await _publishersRepository.GetAll(queryHandler);

            var mappedPublishers = _mapper.Map<List<PublishersDTO>>(publishers.Data);

            if (publishers.PageNumber <= 0 || publishers.ItemsPerpage <= 0 || publishers.Data.Count == 0)
                return ResultService.NotFound<PublishersDTO>("Nunuhm registro encontrada!");

            var paginationHeader = new PaginationHeader<PublishersDTO>(
                publishers.PageNumber,
                publishers.ItemsPerpage,
                publishers.TotalCount,
                publishers.TotalPages
            );

            var result = ResultService.OKPage<PublishersDTO>(mappedPublishers, paginationHeader);

            return result;
        }

        public async Task<ResultService<List<PublishersSummaryDTO>>> GetSummaryPublishersAsync()
        {
            var books = await _publishersRepository.GetSummaryPublishers();
            return ResultService.Ok<List<PublishersSummaryDTO>>(_mapper.Map<List<PublishersSummaryDTO>>(books));
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var publishers = await _publishersRepository.GetById(id);
            if (publishers == null)
                return ResultService.NotFound("Editora não encontrado!");

            return ResultService.Ok(_mapper.Map<PublishersDTO>(publishers));
        }

        public async Task<ResultService> UpdateAsync(PublishersUpdateDTO publisherDTO)
        {
            if (publisherDTO == null)
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var validation = new PublishersDTOValidator().Validate(publisherDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var publisher = await _publishersRepository.GetById(publisherDTO.Id);
            if (publisher == null)
                return ResultService.NotFound("Editora não encontrado!");

            publisher = _mapper.Map(publisherDTO, publisher);
            await _publishersRepository.Update(publisher);
            return ResultService.Ok("Editora atualizado com sucesso.");

        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var publisher = await _publishersRepository.GetById(id);

            if (publisher == null)
                return ResultService.NotFound("Editora não encontrada!");

            var booksAssociatedWithPublisher = await _booksRepository.GetByPublishersId(id);

            if (booksAssociatedWithPublisher.Count > 0)
                return ResultService.BadRequest("A editora não pode ser excluída, pois está associada a livros!");

            await _publishersRepository.Delete(publisher);

            return ResultService.Ok($"Editora com ID {id} foi excluída com sucesso.");
        }


    }
}
