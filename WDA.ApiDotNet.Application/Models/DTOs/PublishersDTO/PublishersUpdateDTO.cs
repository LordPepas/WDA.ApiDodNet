#pragma warning disable CS8618

namespace WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO
{
    public class PublishersUpdateDTO
    {
        public PublishersUpdateDTO(int? id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }

        public int? Id { get; private set; }
        public string Name { get; private set; }
        public string City { get; private set; }
    }
}

