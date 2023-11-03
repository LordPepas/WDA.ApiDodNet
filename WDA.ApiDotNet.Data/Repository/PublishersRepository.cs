#pragma warning disable CS8603

using Microsoft.EntityFrameworkCore;
using WDA.ApiDotNet.Business.Helpers;
using WDA.ApiDotNet.Business.Interfaces.IRepository;
using WDA.ApiDotNet.Business.Models;
using WDA.ApiDotNet.Data.Context;


namespace WDA.ApiDotNet.Data.Repository
{
    public class PublishersRepository : IPublishersRepository
    {
        private readonly ContextDb _db;

        public PublishersRepository(ContextDb db)
        {
            _db = db;
        }

        public async Task Create(Publishers publisher)
        {
            _db.Add(publisher);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Publishers publisher)
        {
            _db.Update(publisher);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Publishers publisher)
        {
            _db.Remove(publisher);
            await _db.SaveChangesAsync();
        }
        public async Task<PageList<Publishers>> GetAll(QueryHandler queryHandler)
        {
            IQueryable<Publishers> query = _db.Publishers.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(queryHandler.SearchValue))
            {
                string searchValueUpper = queryHandler.SearchValue.ToUpper();

                query = query.Where(p =>
                    p.Id.ToString().Contains(searchValueUpper) ||
                    p.Name.ToUpper().Contains(searchValueUpper) ||
                    p.City.ToUpper().Contains(searchValueUpper)
                );
            }

            if (!string.IsNullOrWhiteSpace(queryHandler.OrderByProperty))
            {
                queryHandler.OrderByProperty = queryHandler.OrderByProperty.ToUpper();

                query = queryHandler.OrderByProperty switch
                {
                    "ID" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                    "NAME" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                    "CITY" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.City) : query.OrderBy(p => p.City),
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

            return await PageList<Publishers>.GetResponseAsync(query, queryHandler.PageNumber, queryHandler.ItemsPerpage);
        }


        public async Task<Publishers> GetById(int? id)
        {
            return await _db.Publishers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Publishers>> GetSummaryPublishers()
        {
            return await _db.Publishers
                    .AsNoTracking()
                    .OrderBy(b => b.Id)
                    .ToListAsync();
        }

        public async Task<List<Publishers>> GetByName(string name)
        {
            return await _db.Publishers.Where(x => x.Name == name).ToListAsync();
        }
    }
}
