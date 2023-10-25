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
            IQueryable<Books> query = _db.Books.Include(x => x.Publisher).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(queryHandler.SearchValue))
            {
                queryHandler.SearchValue = queryHandler.SearchValue.ToUpper();

                query = query.Where(p =>
                    p.Id.ToString().Contains(queryHandler.SearchValue) ||
                    p.Name.ToUpper().Contains(queryHandler.SearchValue) ||
                    p.Author.ToUpper().Contains(queryHandler.SearchValue) ||
                    p.Quantity.ToString().Contains(queryHandler.SearchValue) ||
                    p.Release.ToString().Contains(queryHandler.SearchValue) ||
                    p.Rented.ToString().Contains(queryHandler.SearchValue) ||
                    p.Publisher.Name.ToUpper().Contains(queryHandler.SearchValue)
                );
            }

            if (!string.IsNullOrWhiteSpace(queryHandler.OrderByProperty))
            {
                queryHandler.OrderByProperty = queryHandler.OrderByProperty.ToUpper();

                query = queryHandler.OrderByProperty switch
                {
                    "ID" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                    "NAME" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "AUTHOR" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Author) : query.OrderBy(p => p.Author),
                    "QUANTITY" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Quantity) : query.OrderBy(p => p.Quantity),
                    "RELEASE" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Release) : query.OrderBy(p => p.Release),
                    "PUBLISHER" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.PublisherId) : query.OrderBy(p => p.PublisherId),
                    _ => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                };
            }
            else
            {
                query = queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
            }
            if(queryHandler.PageNumber < 1 || queryHandler.ItemsPerpage < 1)
            {
                queryHandler.ItemsPerpage = 0;
            }
            return await PageList<Books>.GetResponseAsync(query, queryHandler.PageNumber, queryHandler.ItemsPerpage);
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

        public async Task<List<Books>> GetSummaryAvailableBooks()
        {
            return await _db.Books
                    .AsNoTracking()
                    .OrderBy(b => b.Id)
                    .Where(b => b.Quantity >= 1)
                    .ToListAsync();
        }

        public async Task<List<Books>> GetByNameAndPublisher(string name,int? publisherId)
        {
            return await _db.Books.Where(x => x.Name == name && x.PublisherId == publisherId).ToListAsync();
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