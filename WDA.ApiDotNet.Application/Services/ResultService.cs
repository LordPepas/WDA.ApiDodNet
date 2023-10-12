#pragma warning disable CS8601

using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WDA.ApiDotNet.Application.Services
{
    public class ResultService : IFilterMetadata
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public static ResultService RequestError(string message, ValidationResult validationResult)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            return new ResultService
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }

        public static ResultService Fail(string message, ICollection<string> errors = null)
        {
            var errorMessages = errors != null ? errors.ToList() : null;
            return new ResultService
            {
                IsSuccess = false,
                Message = message,
                Errors = errorMessages
            };
        }

        public static ResultService Ok(string message) => new ResultService { IsSuccess = true, Message = message };
        public static ResultService<T> Ok<T>(T data) => new() { IsSuccess = true, Data = data };
    }
    public class ResultService<T> : ResultService
    {
        public T Data { get; set; }
    }
}
