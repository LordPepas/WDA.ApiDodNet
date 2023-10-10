namespace WDA.ApiDotNet.Application.Models.DTOs.BooksDTO
{
    public class BooksUpdateDTO
    {
        public BooksUpdateDTO(int id, int publisherId, string name, string author, int launch, int quantity)
        {
            Id = id;
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
        public int Rented { get; private set; }
    }
}
