#pragma warning disable CS8600

using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
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
                return ResultService.Fail("Objeto deve ser informado corretamente!");

            var mappedPublisher = _mapper.Map<Publishers>(publisherDTO);

            var result = new PublishersCreateDTOValidator().Validate(publisherDTO);
            if (!result.IsValid)
                return ResultService.RequestError("Problemas de validação", result);

            var name = await _publishersRepository.GetByName(publisherDTO.Name);
            if (name.Count > 0)
                return ResultService.Fail("Editora já existente!");

            await _publishersRepository.Create(mappedPublisher);
            return ResultService.Ok("Editora adicionado com sucesso.");
        }

        public async Task<ResultService<PublishersDTO>> GetAsync(QueryHandler queryHandler)
        {
            var publishers = await _publishersRepository.GetAll(queryHandler);

            var mappedPublishers = _mapper.Map<List<PublishersDTO>>(publishers.Data);

            var paginationHeader = new PaginationHeader<PublishersDTO>(
                publishers.CurrentPage,
                publishers.PageSize,
                publishers.TotalCount,
                publishers.TotalPages
            );

            CustomHeaders<PublishersDTO> customHeaders = null;

            if (!string.IsNullOrWhiteSpace(queryHandler.Filter.OrderBy) || !string.IsNullOrWhiteSpace(queryHandler.Filter.SearchValue))
            {
                customHeaders = new CustomHeaders<PublishersDTO>(
                    queryHandler.Filter.OrderBy,
                    queryHandler.Filter.SearchValue
                );
            }

            var result = ResultService.OKPage<PublishersDTO>(paginationHeader, mappedPublishers, customHeaders);

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
                return ResultService.Fail("Editora não encontrado!");

            return ResultService.Ok(_mapper.Map<PublishersDTO>(publishers));
        }

        public async Task<ResultService> UpdateAsync(PublishersUpdateDTO publisherDTO)
        {
            if (publisherDTO == null)
                return ResultService.Fail("Objeto deve ser informado corretamente!");

            var validation = new PublishersDTOValidator().Validate(publisherDTO);
            if (!validation.IsValid)
                return ResultService.RequestError("Problemas de validação", validation);

            var publisher = await _publishersRepository.GetById(publisherDTO.Id);
            if (publisher == null)
                return ResultService.Fail("Editora não encontrado!");

            publisher = _mapper.Map(publisherDTO, publisher);
            await _publishersRepository.Update(publisher);
            return ResultService.Ok("Editora atualizado com sucesso.");

        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var publisher = await _publishersRepository.GetById(id);

            if (publisher == null)
                return ResultService.Fail("Editora não encontrada!");

            var booksAssociatedWithPublisher = await _booksRepository.GetByPublishersId(id);

            if (booksAssociatedWithPublisher.Count > 0)
                return ResultService.Fail("A editora não pode ser excluída, pois está associada a livros!");

            await _publishersRepository.Delete(publisher);

            return ResultService.Ok($"Editora com ID {id} foi excluída com sucesso.");
        }


    }
}
