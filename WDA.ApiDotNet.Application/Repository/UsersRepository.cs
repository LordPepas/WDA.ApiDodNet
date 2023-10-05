using Microsoft.EntityFrameworkCore;
using WDA.ApiDodNet.Application.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
using WDA.ApiDotNet.Application.Helpers;
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

        public async Task<Users> CreateAsync(Users user)
        {
            _db.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<Users> UpdateAsync(Users user)
        {
            _db.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task<Users> GetByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageList<Users>> GetAllAsync(PageParams pageParams, string? value)
        {
            IQueryable<Users> query = _db.Users
                .AsNoTracking()
                .OrderBy(b => b.Id);

            if (!string.IsNullOrWhiteSpace(value))
            {
                query = query.Where(p =>
                p.Id.ToString().ToUpper().Contains(value) ||
                p.Name.ToUpper().Contains(value) ||
                p.City.ToUpper().Contains(value) ||
                p.Address.ToUpper().Contains(value) ||
                p.Email.ToUpper().Contains(value)
                );
            };

            query = query.OrderBy(b => b.Id);

            return await PageList<Users>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }
        public async Task DeleteAsync(Users user)
        {
            _db.Remove(user);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Users>> GetByEmailAsync(string email)
        {
            return await _db.Users.Where(x => x.Email == email).ToListAsync();
        }
    }
}
