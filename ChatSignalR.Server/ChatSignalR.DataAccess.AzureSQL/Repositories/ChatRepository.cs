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
    public class ChatRepository : IChatRepository
    {
        private readonly ChatDbContext _context;

        public ChatRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Chat obj)
        {
            if (String.IsNullOrWhiteSpace(obj.Name))
            {
                throw new ArgumentNullException(nameof(obj.Name));
            }

            obj.CreatedDate = DateTime.UtcNow;

            await _context.Chats.AddAsync(obj);
            return obj.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _context.Chats.Where(user => user.Id == id).ExecuteDeleteAsync() > 0;
            return result;
        }

        public async Task<bool> Update(Chat obj)
        {
            var result = await _context.Chats
                .Where(user => user.Id == obj.Id)
                .ExecuteUpdateAsync(item =>
                    item.SetProperty(item => item.Name, obj.Name)) > 0;
            return result;
        }

        public async Task<Chat?> Get(Guid id)
        {
            return await _context.Chats
                .Include(user => user.Messages)
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Id == id);
        }

        public async Task<Chat?> Get(string? name)
        {
            return await _context.Chats
                .Include(user => user.Messages)
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Name == name);
        }

        public async Task<IEnumerable<Chat>> GetAll()
        {
            return await _context.Chats.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Chat>> GetByFilter(string? nameContains = "", int pageNum = 1, int pageSize = 100)
        {
            var query = _context.Chats.AsNoTracking();

            if (!String.IsNullOrWhiteSpace(nameContains))
            {
                query = query.Where(item => item.Name.ToLower().Contains(nameContains.ToLower()));
            }

            query = query.Skip((pageNum - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}
