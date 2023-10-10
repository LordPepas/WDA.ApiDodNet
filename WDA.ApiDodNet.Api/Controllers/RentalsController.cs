﻿using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Interfaces.IServices;
using WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO;

namespace WDA.ApiDotNet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RentalsController : ControllerBase
    {
        private readonly IRentalsService _service;
        private readonly IRentalsRepository _repository;

        public RentalsController(IRentalsService rentalsService, IRentalsRepository rentalsRepository)
        {
            _service = rentalsService;
            _repository = rentalsRepository;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create Rental")]
        public async Task<ActionResult> Post([FromBody] RentalsCreateDTO rentalsDTO)
        {
            var result = await _service.CreateAsync(rentalsDTO);

            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet]
        [SwaggerOperation(Summary = "List Rentals")]
        public async Task<ActionResult> GetAsync([FromQuery] string? search, [FromQuery] PageParams pageParams)
        {
            var rentals = await _repository.GetAllAsync(pageParams, search);
            var result = await _service.GetAsync(pageParams, search);
            if (result.IsSucess)
            {
                Response.AddPagination(rentals.CurrentPage, rentals.PageSize, rentals.TotalCount, rentals.TotalPages);
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("selectBooks")]
        [SwaggerOperation(Summary = "List Select Books")]
        public async Task<ActionResult> GetSelectBooksAsync()
        {
            var result = await _service.GetSelectBooksAsync();
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("selectUsers")]
        [SwaggerOperation(Summary = "List Select Users")]
        public async Task<ActionResult> GetSelectUsersAsync()
        {
            var result = await _service.GetSelectUsersAsync();
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "List Rental")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update rental")]
        public async Task<ActionResult> Put([FromBody] RentalsUpdateDTO rentalsUpdateDTO)
        {

            var result = await _service.UpdateAsync(rentalsUpdateDTO);

            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result.Message);
        }



        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete Rental")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.IsSucess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
