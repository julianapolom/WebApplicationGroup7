
using DataAcces.Intcomex.Interfaces;
using Entity.Intcomex.Models;
namespace DataAcces.Intcomex.Class
{
    public class UnitOfWork : IUnitOfWork
    {
        private IntcomexContext _context;
        private IRepository<Client> _clients;
        private IRepository<ContractClient> _contracts;
        public IRepository<Client> Clients
        {
            get
            {
                return _clients == null ?
                    _clients = new Repository<Client>(_context) :
                    _clients;
            }
        }
        public IRepository<ContractClient> Contracts
        {
            get
            {
                return _contracts == null ?
                    _contracts = new Repository<ContractClient>(_context) :
                    _contracts;
            }
        }

        public UnitOfWork(IntcomexContext context)
        {
            this._context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
