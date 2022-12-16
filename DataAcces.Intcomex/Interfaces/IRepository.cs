namespace DataAcces.Intcomex.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetById(int id);
        bool Add(TEntity entity, out string msError);
        bool Update(TEntity entity, out string msError);
        bool Delete(int id, out string msError);
        void Save();
    }
}
