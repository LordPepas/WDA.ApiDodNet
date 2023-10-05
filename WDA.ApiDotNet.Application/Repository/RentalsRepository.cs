﻿using Microsoft.EntityFrameworkCore;
using WDA.ApiDodNet.Application.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Infra.Data.Context;

namespace WDA.ApiDotNet.Infra.Data.Repository
{
    public class RentalsRepository : IRentalsRepository
    {
        private readonly ContextDb _db;

        public RentalsRepository(ContextDb db)
        {
            _db = db;
        }

        public async Task<Rentals> CreateAsync(Rentals rental)
        {
            var book = await _db.Books.FindAsync(rental.BookId);
            book.Quantity--;
            book.Rented++;
            _db.Books.Update(book);
            _db.Add(rental);
            await _db.SaveChangesAsync();
            return rental;
        }


        public async Task DeleteAsync(Rentals rental)
        {
            _db.Remove(rental);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Rentals>> GetByBookIdAsync(int bookId)
        {
            return await _db.Rentals.Where(x => x.UserId == bookId).ToListAsync();
        }

        public async Task<Rentals> GetByIdAsync(int id)
        {
            return await _db.Rentals
                .Include(x => x.Book)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageList<Rentals>> GetAllAsync(PageParams pageParams, string? value)
        {
            IQueryable<Rentals> query = _db.Rentals
                .Include(x => x.Book)
                .Include(x => x.User)
                .AsNoTracking()
                .OrderBy(b => b.Id);

            //if (!string.IsNullOrWhiteSpace(value))
            //{
            //    query = query.Where(p =>
            //    p.Id.ToString().Contains(value) ||
            //    p.BookId.ToString().Contains(value) ||
            //    p.Book.Name.ToUpper().Contains(value) ||
            //    p.UserId.ToString().Contains(value) ||
            //    p.User.Name.ToUpper().Contains(value) ||
            //    p.RentalDate.ToString().Contains(value) ||
            //    p.PrevisionDate.ToString().Contains(value) ||
            //    p.ReturnDate.ToString().Contains(value)
            //    );
            //};

            return await PageList<Rentals>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }


        public async Task<List<Rentals>> GetByUserIdAsync(int userId)
        {
            return await _db.Rentals.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Rentals> UpdateAsync(Rentals rental)
        {
            var book = await _db.Books.FindAsync(rental.BookId);
            book.Quantity++;
            _db.Books.Update(book);
            _db.Update(rental);
            await _db.SaveChangesAsync();
            return rental;
        }
        public async Task<bool> GetStatus(string forecastDateParam, string returnDateParam)
        {
            if (DateTime.TryParse(forecastDateParam, out DateTime forecastDate) && DateTime.TryParse(returnDateParam, out DateTime returnDate))
            {
                if (returnDate > forecastDate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        public async Task<bool> CheckDate(DateTime date)
        {
            DateTime today = DateTime.Now.Date;

            if (date.Date != today)
            {
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<bool?> CheckPrevisionDate(DateTime previsionDate, DateTime rentalDate)
        {
            if (previsionDate < rentalDate)
            {
                return await Task.FromResult<bool?>(false);
            }

            var diff = previsionDate - rentalDate;
            if (diff.Days > 30)
            {
                return await Task.FromResult<bool?>(true);
            }

            return await Task.FromResult<bool?>(null);
        }

        public async Task<bool> GetStatus(DateTime forecastDate, DateTime returnDate)
        {
            if (returnDate > forecastDate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}