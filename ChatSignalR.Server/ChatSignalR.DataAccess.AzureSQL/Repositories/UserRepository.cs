using ChatSignalR.DataAccess.AzureSQL.Models;
using Microsoft.EntityFrameworkCore;
using ChatSignalR.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.DataAccess.AzureSQL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _context;

        public UserRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(User obj)
        {
            if(String.IsNullOrWhiteSpace(obj.Name))
            { 
                throw new ArgumentNullException(nameof(obj.Name));
            }

            await _context.Users.AddAsync(obj);
            return obj.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _context.Users.Where(user => user.Id == id).ExecuteDeleteAsync() > 0;
            return result;
        }

        public async Task<bool> Update(User obj)
        {
            var result = await _context.Users
                .Where(user => user.Id == obj.Id)
                .ExecuteUpdateAsync(item => 
                    item.SetProperty(item => item.Name, obj.Name)) > 0;
            return result;
        }

        public async Task<User?> Get(Guid id)
        {
            return await _context.Users
                .Include(user => user.Messages)
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User?> Get(string? name)
        {
            return await _context.Users
                .Include(user => user.Messages)
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Name == name);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<User>> GetByFilter(string? nameContains = "", int pageNum = 1, int pageSize = 100)
        {
            var query = _context.Users.AsNoTracking();

            if (!String.IsNullOrWhiteSpace(nameContains))
            {
                query = query.Where(item => item.Name.ToLower().Contains(nameContains.ToLower()));
            }

            query = query.Skip((pageNum-1)*pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}
