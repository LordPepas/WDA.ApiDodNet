#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace WDA.ApiDotNet.Application.Models{
    public class Books
    {
        public Books() { }
        public Books(int id, int publisherId, string name, string author, int release, int quantity,int rented)
        {
            Id = id;
            PublisherId = publisherId;
            Name = name;
            Author = author;
            Release = release;
            Quantity = quantity;
            Rented = rented;

        }

        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int PublisherId { get; private set; }
        public Publishers Publisher { get; private set; }
        public string Author { get; private set; }
        public int Release { get; private set; }
        public int Quantity { get; set; }
        public int Rented { get; set; }
    }
}
