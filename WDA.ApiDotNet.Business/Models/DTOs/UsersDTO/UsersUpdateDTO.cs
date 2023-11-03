#pragma warning disable CS8618

namespace WDA.ApiDotNet.Business.Models.DTOs.UsersDTO
{
    public class UsersUpdateDTO
    {
        public UsersUpdateDTO(int? id, string name, string city, string address, string email)
        {
            Id = id;
            Name = name;
            City = city;
            Address = address;
            Email = email;
        }

        public int? Id { get; set; }
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
    }
}

