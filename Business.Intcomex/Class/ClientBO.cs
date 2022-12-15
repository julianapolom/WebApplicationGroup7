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
        public bool Add(string pClient)
        {
            Client client = JsonConvert.DeserializeObject<Client>(pClient);
            bool result = _uow.Clients.Add(client);
            _uow.Save();
            return result;
        }

        public bool Update(Client pClient)
        {
            return _uow.Clients.Update(pClient);
        }

        public bool Delete(int pId)
        {
            return _uow.Clients.Delete(pId);
        }
    }
}
