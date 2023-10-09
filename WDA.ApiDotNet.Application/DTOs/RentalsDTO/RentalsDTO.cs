using System;

namespace WDA.ApiDotNet.Application.DTOs.RentalsDTO
{
    public class RentalsDTO
    {
        public RentalsDTO()
        {

        }

        public int Id { get; private set; }
        public int BookId { get; private set; }
        public BookRentalDTO Book { get; private set; }
        public int UserId { get; private set; }
        public UserRentalDTO User { get; private set; }
        public string RentalDate { get; private set; }
        public string PrevisionDate { get; private set; }
        public string? ReturnDate { get; private set; }
        public string Status { get; private set; }
    }

    public class BookRentalDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class UserRentalDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
