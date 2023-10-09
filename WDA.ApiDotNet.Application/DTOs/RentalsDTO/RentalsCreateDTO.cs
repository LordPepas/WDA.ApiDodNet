
namespace WDA.ApiDotNet.Application.DTOs.RentalsDTO
{
    public class RentalsCreateDTO
    {
        public RentalsCreateDTO(int bookId, int userId, DateTime rentalDate, DateTime previsionDate)
        {
            BookId = bookId;
            UserId = userId;
            //RentalDate = DateTime.Parse(rentalDate).Date;
            //PrevisionDate = DateTime.Parse(previsionDate).Date;
            RentalDate = rentalDate;
            PrevisionDate = previsionDate;
        }

        public int Id { get; private set; }
        public int BookId { get; private set; }
        public int UserId { get; private set; }
        public DateTime RentalDate { get; private set; }
        public DateTime PrevisionDate { get; private set; }
        public DateTime? ReturnDate { get; private set; } = null;
    }
}
