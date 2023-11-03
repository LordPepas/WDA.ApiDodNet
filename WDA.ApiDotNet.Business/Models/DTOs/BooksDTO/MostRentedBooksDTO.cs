#pragma warning disable CS8618

namespace WDA.ApiDotNet.Business.Models.DTOs.BooksDTO
{
    public class MostRentedBooksDTO
    {
        public MostRentedBooksDTO() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Rented { get; set; }
    }
}
