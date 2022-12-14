using DataAcces.Intcomex.Interfaces;
using Entity.Intcomex.Models;

namespace Business.Intcomex.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Client> Clients { get; }
        IRepository<ContractClient> Contracts { get; }
        void Save();
    }
}
