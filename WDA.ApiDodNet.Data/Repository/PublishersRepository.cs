using Microsoft.EntityFrameworkCore;
using WDA.ApiDodNet.Data.Repositories.Interface;
using WDA.ApiDodNet.Data.Models;
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

        public async Task<Publishers> CreateAsync(Publishers publisher)
        {
            _db.Add(publisher);
            await _db.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publishers> UpdateAsync(Publishers publisher)
        {
            _db.Update(publisher);
            await _db.SaveChangesAsync();
            return publisher;
        }
        public async Task<Publishers>GetByIdAsync(int id)
        {
            return await _db.Publishers.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<ICollection<Publishers>> GetByIdAsync()
        {
            return await _db.Publishers.ToListAsync();
        }
        public async Task DeleteAsync(Publishers publisher)
        {
            _db.Remove(publisher);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Publishers>> GetByNameAsync(string name)
        {
            return await _db.Publishers.Where(x => x.Name == name).ToListAsync();
        }
    }
}
