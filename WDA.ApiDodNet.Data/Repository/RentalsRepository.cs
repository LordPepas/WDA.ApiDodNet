using Microsoft.EntityFrameworkCore;
using WDA.ApiDodNet.Data.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
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

        public async Task<ICollection<Rentals>> GetByRentalsAsync()
        {
            return await _db.Rentals
                .Include(x => x.Book)
                .Include(x => x.User)
                .ToListAsync();
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
    }
}
