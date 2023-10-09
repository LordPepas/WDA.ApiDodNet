namespace WDA.ApiDotNet.Application.DTOs
{
    public class BooksCountDTO
    {
        public BooksCountDTO(int id, string name, int rented)
        {
            Id = id;
            Name = name;
            Rented = rented;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Rented { get; private set; }
    }
}
