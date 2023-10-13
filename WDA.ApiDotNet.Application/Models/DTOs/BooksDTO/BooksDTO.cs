#pragma warning disable CS8618

using WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO;

namespace WDA.ApiDotNet.Application.Models.DTOs.BooksDTO
{
    public class BooksDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublisherId { get; set; }
        public PublishersSummaryDTO Publisher { get; set; }
        public string Author { get; set; }
        public int Release { get; set; }
        public int Quantity { get; set; }
        public int Rented { get; set; }
    }
}
