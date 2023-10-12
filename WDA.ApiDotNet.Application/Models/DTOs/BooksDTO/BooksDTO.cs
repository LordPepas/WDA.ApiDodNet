#pragma warning disable CS8618
namespace WDA.ApiDotNet.Application.Models.DTOs.BooksDTO
{
    public class BooksDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PublisherId { get; set; }
        public BookPublisherDTO Publisher { get; set; }
        public string Author { get; set; }
        public int Launch { get; set; }
        public int Quantity { get; set; }
        public int Rented { get; set; }
    }

    public class BookPublisherDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
