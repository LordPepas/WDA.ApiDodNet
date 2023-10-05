using Microsoft.AspNetCore.Mvc;
using WDA.ApiDodNet.Application.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Services.Interface;

namespace WDA.ApiDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _service;
        private readonly IUsersRepository _repository;

        public UsersController(IUsersService usersService,IUsersRepository repository)
        {
            _service = usersService;
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsersCreateDTO usersDTO)
        {
            var result = await _service.CreateAsync(usersDTO);

            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]

        public async Task<ActionResult> GetAsync([FromQuery] string? value, [FromQuery] PageParams pageParams)
        {
            var users = await _repository.GetAllAsync(pageParams, value);
            var result = await _service.GetAsync(pageParams, value);
            if (result.IsSucess)
            {
                Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);
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
        public async Task<ActionResult> UpdateAsync([FromBody] UsersUpdateDTO usersDTO)
        {
            var result = await _service.UpdateAsync(usersDTO);

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
    }
}
