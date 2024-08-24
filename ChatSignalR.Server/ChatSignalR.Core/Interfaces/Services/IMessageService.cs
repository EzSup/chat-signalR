using ChatSignalR.Core.DTOs;
using ChatSignalR.Core.Interfaces.Repositories;
using ChatSignalR.DataAccess.AzureSQL.Models;
using ChatSignalR.Server.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Interfaces.Services
{
    public interface IMessageService
    {
        Task<Message?> Get(Guid id);
        Task<Guid> RegisterMessage(MessageCreateDto dto);
        Task<IEnumerable<Message>> GetChatHistory(string chatName);
    }
}
