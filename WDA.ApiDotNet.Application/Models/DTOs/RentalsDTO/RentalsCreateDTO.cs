namespace WDA.ApiDotNet.Application.Models.DTOs.RentalsDTO
{
    public class RentalsCreateDTO
    {
        public RentalsCreateDTO(int bookId, int userId, DateTime rentalDate, DateTime previsionDate)
        {
            BookId = bookId;
            UserId = userId;

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
