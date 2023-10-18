#pragma warning disable CS8618

using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;

namespace WDA.ApiDotNet.Application.Models.DTOs.BooksDTO
{
    public class BooksAvailableDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
    }
}
