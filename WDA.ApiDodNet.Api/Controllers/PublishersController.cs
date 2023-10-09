using Microsoft.AspNetCore.Mvc;
using WDA.ApiDodNet.Application.Repositories.Interface;
using WDA.ApiDotNet.Application.DTOs.PublishersDTO;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Services.Interface;

namespace WDA.ApiDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _service;
        private readonly IPublishersRepository _repository;

        public PublishersController(IPublishersService publisherService, IPublishersRepository publishersRepository)
        {
            _service = publisherService;
            _repository = publishersRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PublishersCreateDTO publisherDTO)
        {
            var result = await _service.CreateAsync(publisherDTO);

            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetAsync([FromQuery] string? search, [FromQuery] PageParams pageParams)
        {
            var publishers = await _repository.GetAllAsync(pageParams, search);
            var result = await _service.GetAsync(pageParams, search);


            if (result.IsSucess)
            {
                Response.AddPagination(publishers.CurrentPage, publishers.PageSize, publishers.TotalCount, publishers.TotalPages);
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] PublishersUpdateDTO publisherDTO)
        {
            var result = await _service.UpdateAsync(publisherDTO);

            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetBookCountAsync()
        {
            var bookCount = await _repository.GetTotalCountAsync();
            return Ok(bookCount);
        }
    }
}
