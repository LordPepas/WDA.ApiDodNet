#pragma warning disable CS8602
#pragma warning disable CS8603
#pragma warning disable CS1998 

using Microsoft.EntityFrameworkCore;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Models;
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

        public async Task Create(Rentals rental)
        {
            var book = await _db.Books.FindAsync(rental.BookId);
            book.Quantity--;
            book.Rented++;
            _db.Books.Update(book);
            _db.Add(rental);
            await _db.SaveChangesAsync();
        }
        public async Task Update(Rentals rental)
        {
            var book = await _db.Books.FindAsync(rental.BookId);
            book.Quantity++;
            _db.Books.Update(book);
            _db.Update(rental);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Rentals rental)
        {
            _db.Remove(rental);
            await _db.SaveChangesAsync();
        }

        public async Task<Rentals> GetById(int? id)
        {
            return await _db.Rentals
                .Include(x => x.Book)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageList<Rentals>> GetAll(QueryHandler queryHandler)
        {
            IQueryable<Rentals> query = _db.Rentals
                .Include(x => x.Book)
                .Include(x => x.User)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(queryHandler.SearchValue))
            {
                queryHandler.SearchValue = queryHandler.SearchValue.ToUpper();
                query = query.Where(p =>
                p.Id.ToString().Contains(queryHandler.SearchValue) ||
                p.BookId.ToString().Contains(queryHandler.SearchValue) ||
                p.Book.Name.ToUpper().Contains(queryHandler.SearchValue) ||
                p.UserId.ToString().Contains(queryHandler.SearchValue) ||
                p.User.Name.ToUpper().Contains(queryHandler.SearchValue) ||
                p.RentalDate.ToString().Contains(queryHandler.SearchValue) ||
                p.PrevisionDate.ToString().Contains(queryHandler.SearchValue) ||
                p.ReturnDate.ToString().Contains(queryHandler.SearchValue)
                );
            };

            if (!string.IsNullOrWhiteSpace(queryHandler.OrderByProperty))
            {
                queryHandler.OrderByProperty = queryHandler.OrderByProperty.ToUpper();

                query = queryHandler.OrderByProperty switch
                {
                    "ID" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                    "BOOK" => query.OrderBy(p => p.BookId),
                    "USER" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.UserId),
                    "RENTALDATE" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.RentalDate),
                    "PREVISIONDATE" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.PrevisionDate),
                    "RETURNDATE" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.ReturnDate),
                    "STATUS" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Status) : query.OrderBy(p => p.Status),
                    _ => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                };
            }
            else
            {
                query = queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
            }

            if (queryHandler.PageNumber < 1 || queryHandler.ItemsPerpage < 1)
            {
                queryHandler.ItemsPerpage = 0;
            }

            return await PageList<Rentals>.GetResponseAsync(query, queryHandler.PageNumber, queryHandler.ItemsPerpage);
        }

        public async Task<List<Rentals>> GetByBookId(int bookId)
        {
            return await _db.Rentals.Where(x => x.UserId == bookId).ToListAsync();
        }

        public async Task<List<Rentals>> GetByUserId(int userId)
        {
            return await _db.Rentals.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> CheckDate(DateTime date)
        {
            DateTime today = DateTime.Now;

            if (date != today)
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
            if (returnDate.Date == forecastDate.Date)
            {
                return true;
            }

            if (returnDate.Date > forecastDate.Date)
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
