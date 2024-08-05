using BookCatalog.Domain;

namespace BookCatalog.Services.Contracts
{
    public interface IBaseService<TEntity, TEntityDTO> where TEntity : BaseModel
    {
        IEnumerable<TEntityDTO> GetAll();

        TEntityDTO GetById(int id);

        TEntityDTO Create(TEntityDTO item);

        TEntityDTO Update(int id, TEntityDTO updateBookDto);

        TEntityDTO Delete(int id);
    }
}
