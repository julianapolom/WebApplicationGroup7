using DataAcces.Intcomex.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataAcces.Intcomex.Class
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Instancia del contexto
        /// </summary>
        private readonly IntcomexContext _context;

        /// <summary>
        /// Tipo de entidad sobre la que opera este conjunto.
        /// </summary>
        private readonly DbSet<TEntity> _dbset;

        /// <summary>
        /// Constructor de la case.
        /// </summary>
        /// <param name="context"></param>
        public Repository(IntcomexContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }

        /// <summary>
        /// Obtiene todos los datos de la entidad desde la base de datos.
        /// </summary>
        /// <returns>Lista de objetos</returns>
        public IEnumerable<TEntity> GetAll() =>
            _dbset.ToList();

        /// <summary>
        /// Obtiene un objeto de la entidad por id
        /// </summary>
        /// <param name="id">id a filtrar</param>
        /// <returns>Objeto buscado</returns>
        public TEntity GetById(int id) =>
             _dbset.Find(id);

        /// <summary>
        /// Crea o inserta registros en la tabla o entidad.
        /// </summary>
        /// <param name="entity"></param>
        public bool Add(TEntity entity, out string msError)
        {
            bool result = false;
            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _dbset.Add(entity);
                    dbTransaction.Commit();
                    _context.ChangeTracker.Entries().ToList().ForEach(e => { e.Reload(); });
                    _context.SaveChanges();
                    msError = string.Empty;                    
                    result = true;
                }
                catch (Exception ex)
                {
                    msError = ex.Message;
                    dbTransaction.Rollback();
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Actualiza el objeto o entidad en base de datos.
        /// </summary>
        /// <param name="entity">Entidad a modificar</param>
        public virtual bool Update(TEntity entity, string connection, out string msError)
        {
            bool result;
            RefreshContext(connection);
            using (IDbContextTransaction dbTransaction = ContextUpdate.Database.BeginTransaction())
            {
                try
                {
                    DbSetUpdate.Attach(entity);
                    ContextUpdate.Entry(entity).State = EntityState.Modified;
                    dbTransaction.Commit();
                    ContextUpdate.SaveChanges();
                    _context.ChangeTracker.Entries().ToList().ForEach(e => { e.Reload(); });
                    msError = string.Empty;
                    result = true;
                }
                catch (Exception ex)
                {
                    msError = ex.Message;
                    dbTransaction.Rollback();
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Elimina objetos por id
        /// </summary>
        /// <param name="id">id a filtrar</param>
        public bool Delete(int id, out string msError)
        {
            bool result = false;
            using (IDbContextTransaction dbTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var entity = _dbset.Find(id);
                    _dbset.Remove(entity);
                    dbTransaction.Commit();
                    _context.SaveChanges();
                    msError = string.Empty;
                    result = true;
                }
                catch (Exception ex)
                {
                    msError = ex.Message;
                    dbTransaction.Rollback();
                    result = false;
                }
            }

            return result;
        }

        /// <summary>
        /// Refresca la información del contexto.
        /// </summary>
        /// <param name="contextUpdate"></param>
        /// <param name="_dbsetUpdate"></param>
        protected void RefreshContext(string connection)
        {
            var options = new DbContextOptionsBuilder<IntcomexContext>()
                   .UseSqlServer(new SqlConnection(connection))
                   .Options;

            ContextUpdate = new IntcomexContext(options);
            DbSetUpdate = ContextUpdate.Set<TEntity>();
        }

        /// <summary>
        /// Contexto que libera al _context actual
        /// </summary>
        protected IntcomexContext ContextUpdate { get; set; }

        /// <summary>
        /// BDSet que librera el _dbset actual
        /// </summary>
        protected DbSet<TEntity> DbSetUpdate { get; set; }
    }
}
