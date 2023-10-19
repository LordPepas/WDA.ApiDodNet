using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;

namespace WDA.ApiDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        private readonly IUsersRepository _repository;

        public UsersController(IUsersService usersService, IUsersRepository usersRepository)
        {
            _service = usersService;
            _repository = usersRepository;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create User")]
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Post([FromBody] UsersCreateDTO usersDTO)
        {
            var result = await _service.CreateAsync(usersDTO);


            if (result.HttpStatusCode == HttpStatusCode.Created)
                return StatusCode(201, result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List Users")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Get([FromQuery] QueryHandler queryHandler)
        {
            var users = await _repository.GetAll(queryHandler);
            var result = await _service.GetAsync(queryHandler);

            if (result.HttpStatusCode == HttpStatusCode.OK)
            {
                Response.AddPagination<UsersDTO>(users.PageNumber, users.ItemsPerpage, users.TotalCount, users.TotalPages);
                return Ok(result);
            }
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }
        [HttpGet("SummaryData")]
        [SwaggerOperation(Summary = "List Summary Users")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetSummary()
        {
            var result = await _service.GetSummaryUsersAsync();

            if (result.HttpStatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "List User")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result.HttpStatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update User")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Put([FromBody] UsersUpdateDTO usersDTO)
        {
            var result = await _service.UpdateAsync(usersDTO);

            if (result.HttpStatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete User")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (result.HttpStatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }
    }
}
