using Microsoft.AspNetCore.Mvc;
using WDA.ApiDodNet.Application.Repositories.Interface;
using WDA.ApiDotNet.Application.DTOs;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Services.Interface;

namespace WDA.ApiDotNet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _service;
        private readonly IBooksRepository _repository;

        public BooksController(IBooksService booksService, IBooksRepository booksRepository)
        {
            _service = booksService;
            _repository = booksRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BooksCreateDTO booksDTO)
        {
            var result = await _service.CreateAsync(booksDTO);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]

        public async Task<ActionResult> GetAsync([FromQuery] string? value, [FromQuery] PageParams pageParams)
        {
            var books = await _repository.GetAllAsync(pageParams, value);
            var result = await _service.GetAsync(pageParams, value);

            if (result.IsSucess)
            {
                Response.AddPagination(books.CurrentPage, books.PageSize, books.TotalCount, books.TotalPages);
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
        public async Task<ActionResult> UpdateAsync([FromBody] BooksUpdateDTO booksDTO)
        {
            var result = await _service.UpdateAsync(booksDTO);

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