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
        /// <summary>
        /// Returns a message from the database
        /// </summary>
        /// <param name="id">ID of the message to retrieve.</param>
        /// <returns>Message object with specified ID</returns>
        Task<Message?> Get(Guid id);
        /// <summary>
        /// Performs all actions to create a message, process it and to store it in database
        /// </summary>
        /// <param name="dto">Data required to create new message item</param>
        /// <returns>Id of the created message</returns>
        Task<Guid> RegisterMessage(MessageCreateDto dto);
        /// <summary>
        /// Retrieves chat history from the database
        /// </summary>
        /// <param name="chatName">Name of the searched chat</param>
        /// <returns>Collection of all messages of the specified chat</returns>
        Task<IEnumerable<Message>> GetChatHistory(string chatName);
    }
}
