using Entity.Intcomex.Models;

namespace DataAcces.Intcomex.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Client> Clients { get; }
        IRepository<ContractClient> Contracts { get; }
        void Save();
    }
}
