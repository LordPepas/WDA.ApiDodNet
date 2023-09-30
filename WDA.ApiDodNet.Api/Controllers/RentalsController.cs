
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WDA.ApiDotNet.Application.DTOs.RentalsDTO;
using WDA.ApiDotNet.Application.Services.Interface;

namespace WDA.ApiDotNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _rentalsService;

        public RentalsController(IRentalsService rentalsService)
        {
            _rentalsService = rentalsService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create Rental")]
        public async Task<ActionResult> Post([FromBody] RentalsCreateDTO rentalsDTO)
        {
            var result = await _rentalsService.CreateAsync(rentalsDTO);

            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]
        [SwaggerOperation(Summary = "List Rentals")]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _rentalsService.GetAsync();
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "List Rental")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _rentalsService.GetByIdAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update rental")]
        public async Task<ActionResult> Put([FromBody] RentalsUpdateDTO rentalsUpdateDTO)
        {

            var result = await _rentalsService.UpdateAsync(rentalsUpdateDTO);

            if (result.IsSucess)
                return Ok(result); // Retorna o DTO atualizado

            return BadRequest(result.Message); // Retorna uma mensagem de erro
        }



        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete Rental")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _rentalsService.DeleteAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
