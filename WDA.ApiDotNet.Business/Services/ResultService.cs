#pragma warning disable CS8618
#pragma warning disable CS8625

using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using WDA.ApiDotNet.Business.Helpers;

namespace WDA.ApiDotNet.Business.Services
{
    public class ResultService : IFilterMetadata
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public static ResultService Created(string message) => new()
        {
            HttpStatusCode = HttpStatusCode.Created,
            Message = message
        };

        public static ResultService<T> Ok<T>(T data)
        {
            return new ResultService<T>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Data = data
            };
        }

        public static ResultService Ok(string message) => new()
        {
            HttpStatusCode = HttpStatusCode.OK,
            Message = message
        };


        public static ResultService<T> OKPage<T>(List<T> data, PaginationHeader<T> header)
        {
            return new ResultService<T>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Data = data,
                PageNumber = header.PageNumber,
                ItemsPerpage = header.ItemsPerpage,
                TotalItems = header.TotalItems,
                TotalPages = header.TotalPages
            };
        }

        public static ResultService BadRequest(ValidationResult validationResult)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            return new ResultService
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Errors = errors
            };
        }

        public static ResultService BadRequest(string errorMessage = null, ICollection<string> errors = null)
        {
            var errorMessages = errors != null ? errors.ToList() : new List<string>();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorMessages.Add(errorMessage);
            }

            return new ResultService
            {
                HttpStatusCode = HttpStatusCode.BadRequest,
                Errors = errorMessages
            };
        }

        public static ResultService NotFound(string errorMessage = null, ICollection<string> errors = null)
        {
            var errorMessages = errors != null ? errors.ToList() : new List<string>();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorMessages.Add(errorMessage);
            }

            return new ResultService
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Errors = errorMessages
            };
        }
        public static ResultService<T> NotFound<T>(string errorMessage = null, ICollection<string> errors = null)
        {
            var errorMessages = errors != null ? errors.ToList() : new List<string>();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorMessages.Add(errorMessage);
            }

            return new ResultService<T>
            {
                HttpStatusCode = HttpStatusCode.NotFound,
                Errors = errorMessages
            };
        }
    }

    public class ResultService<T> : ResultService
    {
        public dynamic? Data { get; set; }
        public int? PageNumber { get; set; }
        public int? ItemsPerpage { get; set; }
        public int? TotalItems { get; set; }
        public int? TotalPages { get; set; }
    }
}
