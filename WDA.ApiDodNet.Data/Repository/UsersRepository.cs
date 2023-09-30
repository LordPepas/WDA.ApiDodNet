using Microsoft.EntityFrameworkCore;
using WDA.ApiDodNet.Data.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
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

        public async Task<ICollection<Users>> GetByUsersAsync()
        {
            return await _db.Users.ToListAsync();
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
