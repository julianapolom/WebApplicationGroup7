using Business.Intcomex.Interfaces;
using Entity.Intcomex.Models;

namespace Business.Intcomex.Class
{
    public class ClientBO : IClientBO
    {
        private readonly IUnitOfWork _uow;
        public ClientBO(UnitOfWork uow)
        {
            _uow = uow;
        }

        public bool Add(Client pClient)
        {
            return _uow.Clients.Add(pClient);
        }

        public List<Client> GetAll() =>
            _uow.Clients.GetAll().ToList();

    }
}
