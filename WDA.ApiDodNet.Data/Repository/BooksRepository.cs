#pragma warning disable CS4014 
#pragma warning disable CS8603

using Microsoft.EntityFrameworkCore;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Models;
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

        public async Task Create(Books book)
        {
            _db.AddAsync(book);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Books book)
        {
            _db.Update(book);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Books book)
        {
            _db.Remove(book);
            await _db.SaveChangesAsync();
        }

        public async Task<PageList<Books>> GetAll(QueryHandler queryHandler)
        {
            IQueryable<Books> query = _db.Books.Include(x => x.Publisher)
                 .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(queryHandler.Filter.SearchValue))
            {
                queryHandler.Filter.SearchValue = queryHandler.Filter.SearchValue.ToUpper();

                query = query.Where(p =>
                    p.Id.ToString().Contains(queryHandler.Filter.SearchValue) ||
                    p.Name.ToUpper().Contains(queryHandler.Filter.SearchValue) ||
                    p.Author.ToUpper().Contains(queryHandler.Filter.SearchValue) ||
                    p.Quantity.ToString().Contains(queryHandler.Filter.SearchValue) ||
                    p.Release.ToString().Contains(queryHandler.Filter.SearchValue) ||
                    p.Rented.ToString().Contains(queryHandler.Filter.SearchValue) ||
                    p.Publisher.Name.ToUpper().Contains(queryHandler.Filter.SearchValue)
                );
            }
            if (!string.IsNullOrWhiteSpace(queryHandler.Filter.OrderBy))
            {
                queryHandler.Filter.OrderBy = queryHandler.Filter.OrderBy.ToUpper();

                query = queryHandler.Filter.OrderBy switch
                {
                    "ID" => query.OrderBy(p => p.Id),
                    "NAME" => query.OrderBy(p => p.Name),
                    "AUTHOR" => query.OrderBy(p => p.Author),
                    "QUANTITY" => query.OrderBy(p => p.Quantity),
                    "REALESE" => query.OrderBy(p => p.Release),
                    "PUBLISHER" => query.OrderBy(p => p.PublisherId),
                    _ => query.OrderBy(p => p.Id),
                };
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            return await PageList<Books>.GetResponseAsync(query, queryHandler.Paging.PageNumber, queryHandler.Paging.PageSize);
        }
        public async Task<Books> GetById(int? id)
        {
            return await _db.Books
              .Include(x => x.Publisher)
             .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Books>> GetSummaryBooks()
        {
            return await _db.Books
                    .AsNoTracking()
                    .OrderBy(b => b.Id)
                    .ToListAsync();
        }

        public async Task<List<Books>> GetByName(string name)
        {
            return await _db.Books.Where(x => x.Name == name).ToListAsync();
        }
        async Task<List<Books>> IBooksRepository.GetByPublishersId(int publisherId)
        {
            return await _db.Books.Where(x => x.PublisherId == publisherId).ToListAsync();
        }

        public async Task<List<Books>> MostRentedBooks()
        {
            return await _db.Books
                 .Where(x => x.Rented != 0)
                 .OrderByDescending(x => x.Rented)
                 .ToListAsync();
        }
    }
}