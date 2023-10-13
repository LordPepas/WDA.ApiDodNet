using Microsoft.EntityFrameworkCore;
using WDA.ApiDotNet.Application.Helpers;
using WDA.ApiDotNet.Application.Interfaces.IRepository;
using WDA.ApiDotNet.Application.Models;
using WDA.ApiDotNet.Infra.Data.Context;

namespace WDA.ApiDotNet.Infra.Data.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ContextDb _db;

        public UsersRepository(ContextDb db)
        {
            _db = db;
        }

        public async Task Create(Users user)
        {
            _db.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Users user)
        {
            _db.Update(user);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Users user)
        {
            _db.Remove(user);
            await _db.SaveChangesAsync();
        }


        public async Task<PageList<Users>> GetAll(QueryHandler queryHandler)
        {
            IQueryable<Users> query = _db.Users
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(queryHandler.Filter.SearchValue))
            {
                queryHandler.Filter.SearchValue = queryHandler.Filter.SearchValue.ToUpper();

                query = query.Where(p =>
                p.Id.ToString().Contains(queryHandler.Filter.SearchValue) ||
                p.Name.Contains(queryHandler.Filter.SearchValue) ||
                p.City.Contains(queryHandler.Filter.SearchValue) ||
                p.Address.Contains(queryHandler.Filter.SearchValue) ||
                p.Email.Contains(queryHandler.Filter.SearchValue)
                );
            };

            if (!string.IsNullOrWhiteSpace(queryHandler.Filter.OrderBy))
            {
                queryHandler.Filter.OrderBy = queryHandler.Filter.OrderBy.ToUpper();

                query = queryHandler.Filter.OrderBy switch
                {
                    "ID" => query.OrderBy(p => p.Id),
                    "NAME" => query.OrderBy(p => p.Name),
                    "CITY" => query.OrderBy(p => p.City),
                    "ADDRESS" => query.OrderBy(p => p.Address),
                    "EMAIL" => query.OrderBy(p => p.Email),
                    _ => query.OrderBy(p => p.Id),
                };
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }

            return await PageList<Users>.GetResponseAsync(query, queryHandler.Paging.PageNumber, queryHandler.Paging.PageSize);
        }

        public async Task<Users> GetById(int? id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Users>> GetSummaryUsers()
        {
            return await _db.Users
                    .AsNoTracking()
                    .OrderBy(b => b.Id)
                    .ToListAsync();
        }

        public async Task<List<Users>> GetByEmail(string email)
        {
            return await _db.Users.Where(x => x.Email == email).ToListAsync();
        }

    }
}
