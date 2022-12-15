namespace DataAcces.Intcomex.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(int id);
    }
}
