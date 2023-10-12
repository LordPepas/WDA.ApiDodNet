#pragma warning disable CS8618

namespace WDA.ApiDotNet.Application.Models.DTOs.BooksDTO
{
    public class BooksUpdateDTO
    {
        public BooksUpdateDTO(int? id, string name, int? publisherId, string author, int? release, int? quantity)
        {
            Id = id;
            Name = name;
            PublisherId = publisherId;
            Author = author;
            Release = release;
            Quantity = quantity;
        }

        public int? Id { get; private set; }
        public string Name { get; private set; }
        public int? PublisherId { get; private set; }
        public string Author { get; private set; }
        public int? Release { get; private set; }
        public int? Quantity { get; private set; }
    }
}
