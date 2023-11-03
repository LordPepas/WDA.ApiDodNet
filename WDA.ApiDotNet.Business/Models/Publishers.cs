#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace WDA.ApiDotNet.Business.Models
{
    public class Publishers
    {
        public Publishers() { }

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
