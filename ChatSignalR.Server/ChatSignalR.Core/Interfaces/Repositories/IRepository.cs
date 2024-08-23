using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatSignalR.Core.Interfaces.Repositories
{
    public interface IRepository<Model> where Model : class
    {
        Task<Guid> Create(Model obj);
        Task<bool> Delete(Guid id);
        Task<bool> Update(Model obj);
        Task<Model?> Get(Guid id);
        Task<IEnumerable<Model>> GetAll();
    }
}
