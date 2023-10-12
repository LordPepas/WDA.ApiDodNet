using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;

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
        [SwaggerOperation(Summary = "Create Book")]
        public async Task<ActionResult> Post([FromBody] BooksCreateDTO booksDTO)
        {
            var result = await _service.CreateAsync(booksDTO);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]
        [SwaggerOperation(Summary = "List Books")]
        public async Task<ActionResult> GetAsync([FromQuery] string? search, [FromQuery] PageParams pageParams)
        {
            var books = await _repository.GetAllAsync(pageParams, search);
            var result = await _service.GetAsync(pageParams, search);

            if (result.IsSuccess)
            {
                Response.AddPagination<BooksDTO>(books.CurrentPage, books.PageSize, books.TotalCount, books.TotalPages);
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("SummaryData")]
        [SwaggerOperation(Summary = "List Summary Books")]
        public async Task<ActionResult> GetSelectBooksAsync()
        {
            var result = await _service.GetSummaryBooksAsync();
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "List Book")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("MostRented")]
        [SwaggerOperation(Summary = "Most Rented Books")]
        public async Task<ActionResult> GetMostRentedBooks()
        {
            var result = await _service.GetMostRentedBooks();
            if (result.IsSuccess)
                return Ok(result);
           return BadRequest(result);
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Update Book")]
        public async Task<ActionResult> UpdateAsync([FromBody] BooksUpdateDTO booksDTO)
        {
            var result = await _service.UpdateAsync(booksDTO);

            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete Book")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}