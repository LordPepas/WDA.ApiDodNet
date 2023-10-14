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

        public static ResultService Ok(string message) => new ()
        {
            IsSuccess = true,
            Message = message
        };


        public static ResultService<T> OKPage<T>(PaginationHeader<T> header, List<T> data, CustomHeaders<T> customHeader)
        {
            return new ResultService<T>
            {
                IsSuccess = true,
                Header = header,
                Data = data,
                CustomHeader = customHeader
            };
        }


        public static ResultService<T> Ok<T>(T data)
        {
            return new ResultService<T>
            {
                IsSuccess = true,
                SingleData = data,
            };
        }
        public static ResultService<T> Ok<T>(List<T> data)
        {
            return new ResultService<T>
            {
                IsSuccess = true,
                Data = data,
            };
        }
    }

    public class ResultService<T> : ResultService
    {
        public List<T> Data { get; set; }
        public T SingleData { get; set; }
        public PaginationHeader<T>? Header { get; set; }
        public CustomHeaders<T>? CustomHeader { get; set; }
    }
}