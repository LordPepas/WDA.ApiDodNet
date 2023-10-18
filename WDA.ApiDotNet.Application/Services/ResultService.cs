#pragma warning disable CS8618
#pragma warning disable CS8625

using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using WDA.ApiDotNet.Application.Helpers;

namespace WDA.ApiDotNet.Application.Services
{
    public class ResultService : IFilterMetadata
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public static ResultService RequestError(ValidationResult validationResult)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            return new ResultService
            {
                IsSuccess = false,
                Errors = errors
            };
        }

        public static ResultService Fail(string errorMessage = null, ICollection<string> errors = null)
        {
            var errorMessages = errors != null ? errors.ToList() : new List<string>();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorMessages.Add(errorMessage);
            }

            return new ResultService
            {
                IsSuccess = false,
                Errors = errorMessages
            };
        }

        public static ResultService Ok(string message) => new()
        {
            IsSuccess = true,
            Message = message
        };

        public static ResultService<T> Ok<T>(T data)
        {
            return new ResultService<T>
            {
                IsSuccess = true,
                Data = data,
            };
        }

        public static ResultService<T> OKPage<T>(List<T> data, PaginationHeader<T> header)
        {
            return new ResultService<T>
            {
                IsSuccess = true,
                Data = data,
                PageNumber = header.PageNumber,
                ItemsPerpage = header.ItemsPerpage,
                TotalItems = header.TotalItems,
                TotalPages = header.TotalPages
            };
        }
    }

    public class ResultService<T> : ResultService
    {
        public dynamic Data { get; set; }
        public PaginationHeader<T> Header { get; set; }
        public int PageNumber { get; set; }
        public int ItemsPerpage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}
