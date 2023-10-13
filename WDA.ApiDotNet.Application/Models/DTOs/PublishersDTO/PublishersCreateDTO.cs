#pragma warning disable CS8618

namespace WDA.ApiDotNet.Application.Models.DTOs.PublishersDTO
{
    public class PublishersCreateDTO
    {
        public PublishersCreateDTO(string name, string city)
        {
            Name = name;
            City = city;
        }

        public int? Id { get; private set; }
        public string Name { get; private set; }
        public string City { get; private set; }
    }
}

