#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace WDA.ApiDotNet.Business.Models{
    public class Books
    {
        public Books() { }
        public Books(int id, string name, int publisherId, string author, int release, int quantity,int rented)
        {
            Id = id;
            Name = name;
            PublisherId = publisherId;
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
