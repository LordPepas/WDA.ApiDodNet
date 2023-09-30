namespace WDA.ApiDotNet.Application.DTOs
{
    public class UsersDTO
    {
        public UsersDTO()
        {
        }

        public int Id { get; set; }
        public string Name { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
    }
}

