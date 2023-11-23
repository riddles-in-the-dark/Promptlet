namespace Promptlet.Domain.Contracts
{
    public interface IDomainService<T>
    {
        public Task<T?> Create(T _object);
        public Task Delete(T _object);
        public Task<T?> Update(T _object);
        public Task<IEnumerable<T>?> GetAll();
        public Task<T?> GetById(int Id);
    }
}
