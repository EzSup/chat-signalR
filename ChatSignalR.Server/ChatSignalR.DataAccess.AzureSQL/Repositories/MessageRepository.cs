﻿using ChatSignalR.Core.Interfaces.Repositories;
using ChatSignalR.DataAccess.AzureSQL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.DataAccess.AzureSQL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _context;

        public MessageRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Create(Message obj)
        {
            if (String.IsNullOrWhiteSpace(obj.MessageContent))
            {
                throw new ArgumentNullException(nameof(obj.MessageContent));
            }
            obj.Created = DateTime.UtcNow;

            await _context.Messages.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _context.Messages.Where(user => user.Id == id).ExecuteDeleteAsync() > 0;
            return result;
        }

        public async Task<bool> Update(Message obj)
        {
            var result = await _context.Messages
                .Where(message => message.Id == obj.Id)
                .ExecuteUpdateAsync(item =>
                    item.SetProperty(item => item.MessageContent, obj.MessageContent)) > 0;
            return result;
        }

        public async Task<Message?> Get(Guid id)
        {
            return await _context.Messages
                .AsNoTracking()
                .SingleOrDefaultAsync(message => message.Id == id);
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _context.Messages.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetByFilter(string? contentContains = "", string? authorNameContains = null, string? chatNameContains = null, int pageNum = 1, int pageSize = 100)
        {
            var query = _context.Messages.AsNoTracking();

            if (!String.IsNullOrWhiteSpace(contentContains))
            {
                query = query.Where(item => item.MessageContent.ToLower().Contains(contentContains.ToLower()));
            }
            if (!String.IsNullOrWhiteSpace(authorNameContains))
            {
                query = query.Where(item => item.AuthorName.ToLower().Contains(authorNameContains.ToLower()));
            }
            if (! String.IsNullOrWhiteSpace(chatNameContains))
            {
                query = query.Where(item => item.ChatName.ToLower().Contains(chatNameContains.ToLower()));
            }

            query = query.Skip((pageNum - 1) * pageSize).Take(pageSize);

            return await query.ToListAsync();
        }
    }
}
