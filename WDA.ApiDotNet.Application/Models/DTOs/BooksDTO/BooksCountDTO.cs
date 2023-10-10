namespace WDA.ApiDotNet.Application.Models.DTOs.BooksDTO
{
    public class BooksCountDTO
    {
        public BooksCountDTO()
        {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Rented { get; set; }
    }
}
