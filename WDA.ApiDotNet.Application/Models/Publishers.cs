using System.ComponentModel.DataAnnotations;

namespace WDA.ApiDotNet.Application.Models
{
    public class Publishers
    {
        public Publishers(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }

        [Key]
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string City { get; private set; }
    }
}
