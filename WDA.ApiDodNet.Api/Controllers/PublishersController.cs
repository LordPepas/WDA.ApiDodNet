using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;

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
        [SwaggerOperation(Summary = "Create Publisher")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Post([FromBody] PublishersCreateDTO publisherDTO)
        {
            var result = await _service.CreateAsync(publisherDTO);

            if (result.IsSuccess)
                return  StatusCode(201, result);

            return StatusCode(400, result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List Publishers")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Get([FromQuery] QueryHandler queryHandler)
        {
            var publishers = await _repository.GetAll(queryHandler);
            var result = await _service.GetAsync(queryHandler);


            if (result.IsSuccess)
            {
                Response.AddPagination<PublishersDTO>(publishers.CurrentPage, publishers.PageSize, publishers.TotalCount, publishers.TotalPages);
                return StatusCode(200, result);
            }

            return StatusCode(404, result);
        }

        [HttpGet("SummaryData")]
        [SwaggerOperation(Summary = "List Summary Publishers")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetSummary()
        {
            var result = await _service.GetSummaryPublishersAsync();
            if (result.IsSuccess)
                return StatusCode(200, result);

            return StatusCode(404, result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "List Publisher")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.IsSuccess)
                return StatusCode(200, result);

            return StatusCode(404, result);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update Publisher")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Put([FromBody] PublishersUpdateDTO publisherDTO)
        {
            var result = await _service.UpdateAsync(publisherDTO);

            if (result.IsSuccess)
                return StatusCode(200, result);

            return StatusCode(400, result);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete Publisher")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.IsSuccess)
                return StatusCode(200, result);

            return StatusCode(400, result);
        }
    }
}
