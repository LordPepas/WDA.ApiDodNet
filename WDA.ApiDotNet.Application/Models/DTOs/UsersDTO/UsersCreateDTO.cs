namespace WDA.ApiDotNet.Application.Models.DTOs.UsersDTO
{
    public class UsersCreateDTO
    {
        public UsersCreateDTO(string name, string city, string address, string email)
        {
            Name = name;
            City = city;
            Address = address;
            Email = email;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
    }
}

