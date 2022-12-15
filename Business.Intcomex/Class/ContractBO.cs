using Business.Intcomex.Interfaces;
using Entity.Intcomex.Models;

namespace Business.Intcomex.Class
{
    public class ContractBO : IContractBO
    {
        private readonly IUnitOfWork _uow;
        public ContractBO(UnitOfWork uow) =>
            _uow = uow;

        public List<ContractClient> GetAll() =>
            _uow.Contracts.GetAll().ToList();

    }
}
