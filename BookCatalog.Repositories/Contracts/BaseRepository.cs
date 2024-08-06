using BookCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.Repositories.Contracts
{
    public abstract class BaseRepository<DbModel> : IBaseRepository<DbModel> where DbModel : BaseModel
    {
        protected BookCatalogContext _context;
        protected DbSet<DbModel> _dbSet;

        protected BaseRepository(BookCatalogContext context)
        {
            _context = context;
            _dbSet = context.Set<DbModel>();
        }

        public virtual DbModel? Get(int id)
            => _dbSet.FirstOrDefault(x => x.Id == id);

        public virtual IEnumerable<DbModel> GetAll()
            => _dbSet.ToList();

        public virtual DbModel Save(DbModel model)
        {
            _context.Attach(model);
            _context.SaveChanges();
            return model;
        }

        public virtual DbModel Update(DbModel model)
        {
            _context.Update(model);
            _context.SaveChanges();
            return model;
        }

        public virtual DbModel Remove(DbModel model)
        {
            var deletedModel = _dbSet.Remove(model).Entity;
            _context.SaveChanges();
            return deletedModel;
        }
    }
}
