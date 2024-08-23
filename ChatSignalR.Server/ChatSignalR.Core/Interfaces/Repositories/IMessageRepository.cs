using ChatSignalR.DataAccess.AzureSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Interfaces.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetByFilter(string? contentContains = "", Guid? authorId = null, Guid? chatId = null, int pageNum = 1, int pageSize = 100);
    }
}
