#pragma warning disable CS8618

using WDA.ApiDotNet.Application.Models.DTOs.BooksDTO;
using WDA.ApiDotNet.Application.Models.DTOs.UsersDTO;

namespace WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO
{
    public class RentalsDTO
    {
        public RentalsDTO()
        {

        }

        public int Id { get; private set; }
        public int BookId { get; private set; }
        public BooksSummaryDTO Book { get; private set; }
        public int UserId { get; private set; }
        public UsersSummaryDTO User { get; private set; }
        public DateTime RentalDate { get; private set; }
        public DateTime PrevisionDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public string Status { get; private set; }
    }
}
