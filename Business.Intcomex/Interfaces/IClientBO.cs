using Entity.Intcomex.Models;

namespace Business.Intcomex.Interfaces
{
    public interface IClientBO
    {
        List<Client> GetAll();

        Task<Client> GetById(int pId);

        bool Add(string pClient, out string msError);

        bool Update(string pClient, out string msError);

        bool Delete(int pClient, out string msError);
    }
}
