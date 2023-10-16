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

            if (!string.IsNullOrWhiteSpace(queryHandler.SearchValue))
            {
                queryHandler.SearchValue = queryHandler.SearchValue.ToUpper();

                query = query.Where(p =>
                p.Id.ToString().Contains(queryHandler.SearchValue) ||
                p.Name.Contains(queryHandler.SearchValue) ||
                p.City.Contains(queryHandler.SearchValue) ||
                p.Address.Contains(queryHandler.SearchValue) ||
                p.Email.Contains(queryHandler.SearchValue)
                );
            };

            if (!string.IsNullOrWhiteSpace(queryHandler.OrderBy))
            {
                queryHandler.OrderBy = queryHandler.OrderBy.ToUpper();

                query = queryHandler.OrderBy switch
                {
                    "ID" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                    "NAME" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Name),
                    "CITY" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.City),
                    "ADDRESS" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Address),
                    "EMAIL" => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Email),
                    _ => queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id),
                };
            }
            else
            {
                query = queryHandler.OrderDesc ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id);
            }
                
            return await PageList<Users>.GetResponseAsync(query, queryHandler.PageNumber, queryHandler.PageSize);
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
