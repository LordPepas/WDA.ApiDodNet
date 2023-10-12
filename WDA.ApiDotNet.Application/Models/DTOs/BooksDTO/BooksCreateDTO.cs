namespace WDA.ApiDotNet.Application.Models.DTOs.BooksDTO
{
    public class BooksCreateDTO
    {
        public BooksCreateDTO(string name, int? publisherId, string author, int? launch, int? quantity)
        {
            Name = name;
            PublisherId = publisherId;
            Author = author;
            Launch = launch;
            Quantity = quantity;
        }

        public string Name { get; private set; }
        public int? PublisherId { get; private set; }
        public string Author { get; private set; }
        public int? Launch { get; private set; }
        public int? Quantity { get; private set; }
    }

}
