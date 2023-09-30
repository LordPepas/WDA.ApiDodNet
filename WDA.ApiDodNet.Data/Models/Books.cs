using System.ComponentModel.DataAnnotations;

namespace WDA.ApiDodNet.Data.Models
{
    public class Books
    {
        public Books(int id, int publisherId, string name, string author, int launch, int quantity)
        {
            Id = id;
            PublisherId = publisherId;
            Name = name;
            Author = author;
            Launch = launch;
            Quantity = quantity;
        }

        [Key]
        public int Id { get; private set; }
        public int PublisherId { get; private set; }
        public Publishers Publisher { get; private set; }
        public string Name { get; private set; }
        public string Author { get; private set; }
        public int Launch { get; private set; }
        public int Quantity { get; set; }
    }
}
