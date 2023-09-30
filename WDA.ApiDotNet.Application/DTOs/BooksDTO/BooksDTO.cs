namespace WDA.ApiDotNet.Application.DTOs.BooksDTO
{
    public class BooksDTO
    {
        public BooksDTO()
        {

        }

        public int Id { get; set; }
        public int PublisherId { get; set; }
        public PublisherBookDTO Publisher { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int Launch { get; set; }
        public int Quantity { get; set; }
    }
}
