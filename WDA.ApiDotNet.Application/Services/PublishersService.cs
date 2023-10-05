using AutoMapper;
using WDA.ApiDodNet.Application.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.DTOs.Validations;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Services.Interface;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                return ResultService.Fail<PublishersCreateDTO>("Objeto deve ser informado!");

            var result = new PublishersCreateDTOValidator().Validate(publishersDTO);
            if (!result.IsValid)
                return ResultService.RequestError<PublishersCreateDTO>("Problemas de validação", result);

            var publisher = _mapper.Map<Publishers>(publishersDTO);

            var name = await _publishersRepository.GetByNameAsync(publishersDTO.Name);
            if (name.Count > 0)
            {
                return ResultService.Fail<PublishersCreateDTO>("Editora já existente");
            }
            var data = await _publishersRepository.CreateAsync(publisher);
            return ResultService.Ok<PublishersCreateDTO>(_mapper.Map<PublishersCreateDTO>(data));
        }

        public async Task<ResultService<ICollection<PublishersDTO>>> GetAsync(string value, PageParams pageParams)
        {
            var publishers = await _publishersRepository.GetAllAsync(pageParams, value);

            return ResultService.Ok<ICollection<PublishersDTO>>(_mapper.Map<ICollection<PublishersDTO>>(publishers));
        }


        public async Task<ResultService<PublishersDTO>> GetByIdAsync(int id)
        {
            var publishers = await _publishersRepository.GetByIdAsync(id);
            if (publishers == null)
                return ResultService.Fail<PublishersDTO>("Editora não encontrado!");

            return ResultService.Ok(_mapper.Map<PublishersDTO>(publishers));
        }



        public async Task<ResultService> UpdateAsync(PublishersUpdateDTO publishersDTO)
        {
            if (publishersDTO == null)
                return ResultService.Fail<PublishersUpdateDTO>("Objeto deve ser informado!");
            var validation = new PublishersDTOValidator().Validate(publishersDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<PublishersUpdateDTO>("Problemas de validação", validation);
            var publishers = await _publishersRepository.GetByIdAsync(publishersDTO.Id);
            if (publishers == null)
                return ResultService.Fail("Editora não encontrado!");

            publishers = _mapper.Map<PublishersUpdateDTO, Publishers>(publishersDTO, publishers);
            await _publishersRepository.UpdateAsync(publishers);
            return ResultService.Ok("Editora atualizado");

        }
        public async Task<ResultService> DeleteAsync(int id)
        {
            var publisher = await _publishersRepository.GetByIdAsync(id);

            if (publisher == null)
            {
                return ResultService.Fail<PublishersDTO>("Editora não encontrada!");
            }

            // Verifique se há livros associados diretamente na lógica de exclusão
            var booksAssociatedWithPublisher = await _booksRepository.GetByPublishersIdAsync(id);

            if (booksAssociatedWithPublisher.Count > 0)
            {
                return ResultService.Fail<PublishersDTO>("A editora não pode ser excluída, pois está associada a livros.");
            }

            await _publishersRepository.DeleteAsync(publisher);

            return ResultService.Ok($"Editora com ID {id} foi excluída com sucesso.");
        }


    }
}
