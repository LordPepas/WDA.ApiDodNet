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

        public async Task<ResultService> CreateAsync(PublishersCreateDTO newPublisherDTO)
        {
            if (newPublisherDTO == null)
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var mappedPublisher = _mapper.Map<Publishers>(newPublisherDTO);

            var validation = new PublishersCreateDTOValidator().Validate(newPublisherDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var duplicateName = await _publishersRepository.GetByName(newPublisherDTO.Name);
            if (duplicateName.Count > 0)
                return ResultService.BadRequest("Editora já existente!");

            await _publishersRepository.Create(mappedPublisher);
            return ResultService.Created("Editora adicionado com sucesso.");
        }

        public async Task<ResultService<PublishersDTO>> GetAsync(QueryHandler queryHandler)
        {
            var result = await _publishersRepository.GetAll(queryHandler);

            var mappedPublishers = _mapper.Map<List<PublishersDTO>>(result.Data);

            if (result.PageNumber <= 0 || result.ItemsPerpage <= 0 || result.Data.Count == 0)
                return ResultService.NotFound<PublishersDTO>("Nenhum registro encontrada!");

            var paginationHeader = new PaginationHeader<PublishersDTO>(
                result.PageNumber,
                result.ItemsPerpage,
                result.TotalCount,
                result.TotalPages
            );

            return ResultService.OKPage<PublishersDTO>(mappedPublishers, paginationHeader);
        }

        public async Task<ResultService<List<PublishersSummaryDTO>>> GetSummaryPublishersAsync()
        {
            var result = await _publishersRepository.GetSummaryPublishers();
            if (result.Count == 0)
                return ResultService.NotFound<List<PublishersSummaryDTO>>("Nenhum registro encontrada!");

            return ResultService.Ok<List<PublishersSummaryDTO>>(_mapper.Map<List<PublishersSummaryDTO>>(result));
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var result = await _publishersRepository.GetById(id);
            if (result == null)
                return ResultService.NotFound("Editora não encontrado!");

            return ResultService.Ok(_mapper.Map<PublishersDTO>(result));
        }

        public async Task<ResultService> UpdateAsync(PublishersUpdateDTO updatePublisherDTO)
        {
            if (updatePublisherDTO == null)
                return ResultService.BadRequest("Objeto deve ser informado corretamente!");

            var validation = new PublishersDTOValidator().Validate(updatePublisherDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var publisher = await _publishersRepository.GetById(updatePublisherDTO.Id);
            if (publisher == null)
                return ResultService.NotFound("Editora não encontrado!");

            publisher = _mapper.Map(updatePublisherDTO, publisher);
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
