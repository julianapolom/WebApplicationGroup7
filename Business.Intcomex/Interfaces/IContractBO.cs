using Entity.Intcomex.Models;

namespace Business.Intcomex.Interfaces
{
    public interface IContractBO
    {
        List<ContractClient> GetAll();
    }
}
