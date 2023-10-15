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

            if (!string.IsNullOrWhiteSpace(queryHandler.SearchValue))
            {
                queryHandler.SearchValue = queryHandler.SearchValue.ToUpper();

                query = query.Where(p =>
                    p.Id.ToString().ToUpper().Contains(queryHandler.SearchValue) ||
                    p.Name.Contains(queryHandler.SearchValue) ||
                    p.City.Contains(queryHandler.SearchValue)
                );
            }
            if (!string.IsNullOrWhiteSpace(queryHandler.OrderBy))
            {
                queryHandler.OrderBy = queryHandler.OrderBy.ToUpper();

                query = queryHandler.OrderBy switch
                {
                    "ID" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                    "NAME" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Name),
                    "CITY" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.City),
                    _ => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                };
            }
            else
            {
                query = queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
            }

            return await PageList<Publishers>.GetResponseAsync(query, queryHandler.PageNumber, queryHandler.PageSize);
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
