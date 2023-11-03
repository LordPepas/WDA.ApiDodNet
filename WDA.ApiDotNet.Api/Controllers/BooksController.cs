using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WDA.ApiDotNet.Business.Helpers;
using WDA.ApiDotNet.Business.Interfaces.IRepository;
using WDA.ApiDotNet.Business.Interfaces.IServices;
using WDA.ApiDotNet.Business.Models.DTOs.BooksDTO;

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
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Post([FromBody] BooksCreateDTO booksDTO)
        {
            var result = await _service.CreateAsync(booksDTO);

            if (result.HttpStatusCode == HttpStatusCode.Created)
                return StatusCode(201, result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List Books")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetAll([FromQuery] QueryHandler queryHandler)
        {
            var books = await _repository.GetAll(queryHandler);
            var result = await _service.GetAsync(queryHandler);

            if (result.HttpStatusCode == HttpStatusCode.OK)
            {
                Response.AddPagination<BooksDTO>(books.PageNumber, books.ItemsPerpage, books.TotalCount, books.TotalPages);
                return Ok(result);
            }
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpGet("SummaryData")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetSummary()
        {
            var result = await _service.GetSummaryBooksAsync();

            if (result.HttpStatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpGet("AvailableData")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetSummaryAvailable()
        {
            var result = await _service.GetSummaryAvailableBooksAsync();

            if (result.HttpStatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetByIdAsync(int id)
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

        [HttpGet("MostRented")]
        [SwaggerOperation(Summary = "Most Rented Books")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> GetMostRented()
        {
            var result = await _service.GetMostRentedBooks();

            if (result.HttpStatusCode == HttpStatusCode.OK)
                return Ok(result);
            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update Book")]
        [SwaggerResponse(200, "OK")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Put([FromBody] BooksUpdateDTO booksDTO)
        {
            var result = await _service.UpdateAsync(booksDTO);

            if (result.HttpStatusCode == HttpStatusCode.OK)
                return Ok(result);

            else if (result.HttpStatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete Book")]
        [SwaggerResponse(200, "OK")]
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
