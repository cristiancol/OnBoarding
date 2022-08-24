using BancoOnBoarding.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BancoOnBoarding.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Add(T data)
        {
            _dbSet.Add(data);
        }
        public void Delete(int id)
        {
            var data = _dbSet.Find(id);
            if (data != null)
                _dbSet.Remove(data);
        }

        public IEnumerable<T> Filter(Func<T, bool> predicate)
        {
            return _dbSet.AsQueryable().Where(predicate);
        }

        public IEnumerable<T> Get() => _dbSet.ToList();

        public T? Get(int id) => _dbSet.Find(id);

        public void Save() => _context.SaveChanges();

        public void Update(T data)
        {
            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
        }
    }
}
