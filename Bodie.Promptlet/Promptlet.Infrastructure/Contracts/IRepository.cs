using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Promptlet.Infrastructure.Data;

namespace Promptlet.Infrastructure.Contracts
{
    public interface IRepository<T>
    {
        public Task<T?> Create(T _object);
        public Task Delete(T _object);
        public Task<T?> Update(T _object);
        public Task<IEnumerable<T>?> GetAll();
        public Task<T?> GetById(int Id);
    }
}
