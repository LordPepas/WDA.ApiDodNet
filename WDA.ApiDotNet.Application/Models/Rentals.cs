﻿using System.ComponentModel.DataAnnotations;

namespace WDA.ApiDotNet.Application.Models
{
    public class Rentals
    {
        public Rentals(int id, int bookId, int userId, DateTime rentalDate, DateTime previsionDate, DateTime? returnDate)
        {
            Id = id;
            BookId = bookId;
            UserId = userId;
            RentalDate = rentalDate;
            PrevisionDate = previsionDate;
            ReturnDate = returnDate;
        }

        [Key]
        public int Id { get; private set; }
        public int BookId { get; private set; }
        public Books Book { get; private set; }
        public int UserId { get; private set; }
        public Users User { get; private set; }
        public DateTime RentalDate { get; private set; }
        public DateTime PrevisionDate { get; private set; }
        public DateTime? ReturnDate { get; private set; }
        public string Status { get; set; }
    }
}
