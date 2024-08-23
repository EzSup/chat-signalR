using ChatSignalR.DataAccess.AzureSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> Get(string? name);
        Task<IEnumerable<User>> GetByFilter(string? nameContains = "", int pageNum = 1, int pageSize = 100);
    }
}
