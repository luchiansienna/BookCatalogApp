using BookCatalog.Domain;

namespace BookCatalog.Repositories.Contracts
{
    public interface IBaseRepository<DbModel> where DbModel : BaseModel
    {
        IEnumerable<DbModel> GetAll();

        DbModel? Get(int id);

        DbModel Save(DbModel model);

        DbModel Update(DbModel model);

        DbModel Remove(DbModel model);
    }
}
