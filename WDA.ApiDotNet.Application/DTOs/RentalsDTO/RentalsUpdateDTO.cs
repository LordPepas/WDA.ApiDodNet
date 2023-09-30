namespace WDA.ApiDotNet.Application.DTOs.RentalsDTO
{
    public class RentalsUpdateDTO
    {
        public RentalsUpdateDTO(int id, DateTime returnDate)
        {
            Id = id;
            ReturnDate = returnDate;
        }

        public int Id { get; private set; }
        public DateTime ReturnDate { get; private set; }
    }
}
