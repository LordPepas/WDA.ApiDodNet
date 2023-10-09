namespace WDA.ApiDotNet.Application.DTOs
{
    public class BooksCreateDTO
    {
        public BooksCreateDTO(int publisherId, string name, string author, int launch, int quantity)
        {
            PublisherId = publisherId;
            Name = name;
            Author = author;
            Launch = launch;
            Quantity = quantity;
        }

        public int Id { get; private set; }
        public int PublisherId { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public int Launch { get; private set; }
        public int Quantity { get; private set; }
        public int Rented { get; private set; } = 0;
    }
    public class PublisherBookDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
