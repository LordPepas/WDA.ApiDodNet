#pragma warning disable CS8603

using Microsoft.EntityFrameworkCore;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Infra.Data.Context;


namespace WDA.ApiDotNet.Infra.Data.Repository
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

            if (!string.IsNullOrWhiteSpace(queryHandler.Filter.SearchValue))
            {
                queryHandler.Filter.SearchValue = queryHandler.Filter.SearchValue.ToUpper();

                query = query.Where(p =>
                    p.Id.ToString().ToUpper().Contains(queryHandler.Filter.SearchValue) ||
                    p.Name.Contains(queryHandler.Filter.SearchValue) ||
                    p.City.Contains(queryHandler.Filter.SearchValue)
                );
            }
            if (!string.IsNullOrWhiteSpace(queryHandler.Filter.OrderBy))
            {
                queryHandler.Filter.OrderBy = queryHandler.Filter.OrderBy.ToUpper();

                query = queryHandler.Filter.OrderBy switch
                {
                    "ID" => query.OrderBy(p => p.Id),
                    "NAME" => query.OrderBy(p => p.Name),
                    "CITY" => query.OrderBy(p => p.City),
                    _ => query.OrderBy(p => p.Id),
                };
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            return await PageList<Publishers>.GetResponseAsync(query, queryHandler.Paging.PageNumber, queryHandler.Paging.PageSize);
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
