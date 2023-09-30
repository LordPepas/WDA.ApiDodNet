using Microsoft.AspNetCore.Mvc;
using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.Services.Interface;

namespace WDA.ApiDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsersCreateDTO usersDTO)
        {
            var result = await _usersService.CreateAsync(usersDTO);

            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]

        public async Task<ActionResult> GetAsync()
        {
            var result = await _usersService.GetAsync();
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _usersService.GetByIdAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromBody] UsersUpdateDTO usersDTO)
        {
            var result = await _usersService.UpdateAsync(usersDTO);

            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _usersService.DeleteAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
