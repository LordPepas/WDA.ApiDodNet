using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WDA.ApiDodNet.Application.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.Helpers;
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

        public async Task<PageList<Books>> GetAllAsync(PageParams pageParams, string? search)
        {
            IQueryable<Books> query = _db.Books.Include(x => x.Publisher)
                 .AsNoTracking()
                 .OrderBy(b => b.Id);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToUpper(); // Converter o valor de pesquisa para maiúsculas

                query = query.Where(p =>
                    p.Id.ToString().Contains(search) ||
                    p.Name.ToUpper().Contains(search) ||
                    p.Author.ToUpper().Contains(search) ||
                    p.Quantity.ToString().Contains(search) ||
                    p.Launch.ToString().Contains(search) ||
                    p.Rented.ToString().Contains(search) ||
                    p.Publisher.Name.ToUpper().Contains(search) // Pesquisar dentro do objeto Publisher
                );
            }
            return await PageList<Books>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
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
        public async Task<int> GetTotalCountAsync()
        {
                var totalCount = await _db.Books.CountAsync();
                return totalCount;
        }
        public async Task<List<Books>> MostRentedBooks()
        {
            var mostRentedBooks = await _db.Books
                .Where(x => x.Rented != null)
                .OrderByDescending(x => x.Rented)
                .ToListAsync();

            return mostRentedBooks;
        }
    }
}