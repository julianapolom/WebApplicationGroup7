using Entity.Intcomex.EntitiesDTO;
using Entity.Intcomex.Models;

namespace Business.Intcomex.Interfaces
{
    public interface IClientBO
    {
        List<ClientDTO> GetAll(out string msError);

        ClientDTO GetById(int pId, out string msError);

        bool Add(string pClient, out string msError);

        bool Update(string pClient, out string msError);

        bool Delete(int pClient, out string msError);
    }
}
