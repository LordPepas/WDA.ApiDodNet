#pragma warning disable CS8618

namespace WDA.ApiDotNet.Business.Models.DTOs.RentalsDTO
{
    public class RentalsCreateDTO
    {
        public RentalsCreateDTO(int? bookId, int? userId, DateTime? previsionDate)
        {
            BookId = bookId;
            UserId = userId;
            PrevisionDate = previsionDate;
            RentalDate = DateTime.Now.Date;
        }

        public int? Id { get; private set; }
        public int? BookId { get; private set; }
        public int? UserId { get; private set; }
        public DateTime RentalDate { get; private set; }
        public DateTime? PrevisionDate { get; private set; }
        public DateTime? ReturnDate { get; private set; } = null;
    }
}
