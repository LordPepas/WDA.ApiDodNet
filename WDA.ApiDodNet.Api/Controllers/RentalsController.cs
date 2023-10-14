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
        [SwaggerResponse(201, "Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Post([FromBody] RentalsCreateDTO rentalsDTO)
        {
            var result = await _service.CreateAsync(rentalsDTO);

            if (result.IsSuccess)
                return StatusCode(201, result);

            return StatusCode(400, result);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "List Rentals")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Get([FromQuery] QueryHandler queryHandler)
        {
            var rentals = await _repository.GetAll(queryHandler);
            var result = await _service.GetAsync(queryHandler);
            if (result.IsSuccess)
            {
                Response.AddPagination<RentalsDTO>(rentals.CurrentPage, rentals.PageSize, rentals.TotalCount, rentals.TotalPages);
                return StatusCode(200, result);
            }
            return StatusCode(404, result);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "List Rental")]
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
        [SwaggerOperation(Summary = "Update rental")]
        [SwaggerResponse(200, "Ok")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        public async Task<ActionResult> Put([FromBody] RentalsUpdateDTO rentalsUpdateDTO)
        {

            var result = await _service.UpdateAsync(rentalsUpdateDTO);

            if (result.IsSuccess)
                return StatusCode(200, result);

            return StatusCode(400, result);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Delete Rental")]
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
