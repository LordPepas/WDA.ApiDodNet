#pragma warning disable CS8600

using AutoMapper;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;
using WDA.ApiDotNet.Business.Models.DTOs.Validations.CreationValidations;
using WDA.ApiDotNet.Business.Models.DTOs.Validations.UpdateValidations;

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
            var mappedPublisher = _mapper.Map<Publishers>(newPublisherDTO);

            var validation = new PublisherCreationValidator().Validate(newPublisherDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);

            var duplicateName = await _publishersRepository.GetByName(newPublisherDTO.Name);
            if (duplicateName.Count > 0)
                return ResultService.BadRequest("Editora já cadastrado.");

            await _publishersRepository.Create(mappedPublisher);
            return ResultService.Created("Editora adicionado com sucesso.");
        }

        public async Task<ResultService<PublishersDTO>> GetAsync(QueryHandler queryHandler)
        {
            var result = await _publishersRepository.GetAll(queryHandler);

            var mappedPublishers = _mapper.Map<List<PublishersDTO>>(result.Data);

            if (result.PageNumber <= 0 || result.ItemsPerpage <= 0 || result.Data.Count == 0)
                return ResultService.NotFound<PublishersDTO>("Nenhum registro encontrada.");

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
                return ResultService.NotFound<List<PublishersSummaryDTO>>("Nenhum registro encontrada.");

            return ResultService.Ok<List<PublishersSummaryDTO>>(_mapper.Map<List<PublishersSummaryDTO>>(result));
        }

        public async Task<ResultService> GetByIdAsync(int id)
        {
            var result = await _publishersRepository.GetById(id);
            if (result == null)
                return ResultService.NotFound("Editora não encontrado.");

            return ResultService.Ok(_mapper.Map<PublishersDTO>(result));
        }

        public async Task<ResultService> UpdateAsync(PublishersUpdateDTO updatedPublisherDTO)
        {
            var publisher = await _publishersRepository.GetById(updatedPublisherDTO.Id);
            if (publisher == null)
                return ResultService.NotFound("Editora não encontrado.");
            if (publisher.Name != updatedPublisherDTO.Name)
            {
                var duplicateName = await _publishersRepository.GetByName(updatedPublisherDTO.Name);
                if (duplicateName.Count > 0)
                    return ResultService.BadRequest("Editora já cadastrado.");
            }

            var validation = new PublisherUpdateValidator().Validate(updatedPublisherDTO);
            if (!validation.IsValid)
                return ResultService.BadRequest(validation);


            publisher = _mapper.Map(updatedPublisherDTO, publisher);
            await _publishersRepository.Update(publisher);
            return ResultService.Ok("Editora atualizado com sucesso.");

        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var publisher = await _publishersRepository.GetById(id);

            if (publisher == null)
                return ResultService.NotFound("Editora não encontrada.");

            var booksAssociatedWithPublisher = await _booksRepository.GetByPublishersId(id);

            if (booksAssociatedWithPublisher.Count > 0)
                return ResultService.BadRequest("Editora está associada a livros.");

            await _publishersRepository.Delete(publisher);

            return ResultService.Ok($"Editora com ID {id} foi excluída com sucesso.");
        }


    }
}
