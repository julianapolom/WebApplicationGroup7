using Business.Intcomex.Interfaces;
using Entity.Intcomex.Models;
using Newtonsoft.Json;

namespace Business.Intcomex.Class
{
    public class ClientBO : IClientBO
    {
        private readonly IUnitOfWork _uow;
        public ClientBO(UnitOfWork uow) =>
            _uow = uow;

        public List<Client> GetAll() =>
            _uow.Clients.GetAll().ToList();

        public async Task<Client> GetById(int pId) =>
            await _uow.Clients.GetById(pId);

        public bool Add(string pClient, out string msError)
        {
            bool result = _uow.Clients.Add(JsonConvert.DeserializeObject<Client>(pClient), out msError);
            _uow.Save();
            return result;
        }

        public bool Update(string pClient, out string msError)
        {
            bool result = _uow.Clients.Update(JsonConvert.DeserializeObject<Client>(pClient), out msError);
            _uow.Save();
            return result;
        }

        public bool Delete(int pId, out string msError)
        {
            bool result = _uow.Clients.Delete(pId, out msError);
            _uow.Save();
            return result;
        }
    }
}
