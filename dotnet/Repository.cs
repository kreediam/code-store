using System.Data;
using System.Data.Entity;
using System.Linq;

namespace GlobalUtil
{
    public class Repository<T> where T : class
    {
        private readonly Context<T> _context;

        public Repository(string connectionString)
        {
            _context = new Context<T>(connectionString);
        }

        public T Get(object key)
        {
            return _context.DbSet.Find(key);
        }

        public IQueryable<T> Query()
        {
            return _context.DbSet;
        }

        public T Add(T entity)
        {
            return _context.DbSet.Add(entity);
        }

        public T Update(T entity)
        {
            var updated = _context.DbSet.Attach(entity);
            _context.DbContext.Entry(entity).State = EntityState.Modified;
            return updated;
        }

        public T Remove(T entity)
        {
            return _context.DbSet.Remove(entity);
        }

        public void SaveChanges()
        {
            _context.DbContext.SaveChanges();
        }
    }

    public class Context<T> where T : class
    {
        public Context(string connectionString)
        {
            this.DbContext = new DbContext(connectionString);
            this.DbSet = DbContext.Set<T>();
        }

        public DbContext DbContext
        {
            get;
            private set;
        }

        public IDbSet<T> DbSet
        {
            get;
            private set;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
