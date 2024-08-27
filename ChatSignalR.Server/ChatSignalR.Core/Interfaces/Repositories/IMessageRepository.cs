using ChatSignalR.DataAccess.AzureSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Interfaces.Repositories
{
    public interface IMessageRepository
    {
        /// <summary>
        /// Creates a new message in the database.
        /// </summary>
        /// <param name="obj">The message object to create.</param>
        /// <returns>The ID of the created message.</returns>
        Task<Guid> Create(Message obj);

        /// <summary>
        /// Deletes a message from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the message to delete.</param>
        /// <returns>A boolean value indicating whether the deletion was successful.</returns>
        Task<bool> Delete(Guid id);

        /// <summary>
        /// Updates an existing message in the database.
        /// </summary>
        /// <param name="obj">The message object with updated data.</param>
        /// <returns>A boolean value indicating whether the update was successful.</returns>
        Task<bool> Update(Message obj);

        /// <summary>
        /// Retrieves a message from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the message to retrieve.</param>
        /// <returns>The message object with the specified ID, or null if not found.</returns>
        Task<Message?> Get(Guid id);

        /// <summary>
        /// Retrieves all messages from the database.
        /// </summary>
        /// <returns>A collection of all messages.</returns>
        Task<IEnumerable<Message>> GetAll();

        /// <summary>
        /// Retrieves messages from the database based on the specified filter criteria.
        /// </summary>
        /// <param name="contentContains">Optional filter to search messages containing specific content.</param>
        /// <param name="authorNameContains">Optional filter to search messages by author name.</param>
        /// <param name="chatNameContains">Optional filter to search messages by chat name.</param>
        /// <param name="pageNum">The page number for pagination (default is 1).</param>
        /// <param name="pageSize">The number of messages per page (default is 100).</param>
        /// <returns>A collection of messages that match the filter criteria.</returns>
        Task<IEnumerable<Message>> GetByFilter(string? contentContains = "", string? authorNameContains = null, string? chatNameContains = null, int pageNum = 1, int pageSize = 100);
    }
}
