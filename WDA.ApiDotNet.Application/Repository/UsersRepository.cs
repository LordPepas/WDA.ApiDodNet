﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<PageList<Users>> GetAllAsync(PageParams pageParams, string? search)
        {
            IQueryable<Users> query = _db.Users
                .AsNoTracking()
                .OrderBy(b => b.Id);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToUpper();

                query = query.Where(p =>
                p.Id.ToString().Contains(search) ||
                p.Name.Contains(search) ||
                p.City.Contains(search) ||
                p.Address.Contains(search) ||
                p.Email.Contains(search)
                );
            };


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
        public async Task<int> GetTotalCountAsync()
        {
            var totalCount = await _db.Users.CountAsync();
            return totalCount;
        }
    }
}
