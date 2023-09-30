using Microsoft.EntityFrameworkCore;
using WDA.ApiDodNet.Data.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Infra.Data.Context;

namespace WDA.ApiDotNet.Infra.Data.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly ContextDb _db;

        public BooksRepository(ContextDb db)
        {
            _db = db;
        }

        public async Task<Books> CreateAsync(Books book)
        {
            _db.Add(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task DeleteAsync(Books book)
        {
            _db.Remove(book);
            await _db.SaveChangesAsync();
        }

        public async Task<Books> GetByIdAsync(int id)
        {
            return await _db.Books
              .Include(x => x.Publisher)
             .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<Books>> GetByBooksAsync()
        {
            return await _db.Books
               .Include(x => x.Publisher)
               .AsNoTracking()
               .ToListAsync();
        }


        public async Task<Books> UpdateAsync(Books book)
        {
            _db.Update(book);
            await _db.SaveChangesAsync();
            return book;
        }

        async Task<List<Books>> IBooksRepository.GetByPublishersIdAsync(int publisherId)
        {
            return await _db.Books.Where(x => x.PublisherId == publisherId).ToListAsync();
        }

        public async Task<List<Books>> GetByNameAsync(string name)
        {
            return await _db.Books.Where(x => x.Name == name).ToListAsync();
        }
    }
}