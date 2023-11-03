#pragma warning disable CS8618

namespace WDA.ApiDotNet.Business.Models.DTOs.BooksDTO
{
    public class BooksCreateDTO
    {
        public BooksCreateDTO(string name, int? publisherId, string author, int? release, int? quantity)
        {
            Name = name;
            PublisherId = publisherId;
            Author = author;
            Release = release;
            Quantity = quantity;
        }

        public string Name { get; private set; }
        public int? PublisherId { get; private set; }
        public string Author { get; private set; }
        public int? Release { get; private set; }
        public int? Quantity { get; private set; }
    }

}
