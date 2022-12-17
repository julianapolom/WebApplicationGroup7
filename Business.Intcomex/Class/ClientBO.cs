using Business.Intcomex.Interfaces;
using Entity.Intcomex.EntitiesDTO;
using Entity.Intcomex.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Tools;

namespace Business.Intcomex.Class
{
    public class ClientBO : IClientBO
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="config"></param>
        public ClientBO(UnitOfWork uow, IConfiguration config)
        {
            _uow = uow;
            _configuration = config;
        }

        /// <summary>
        /// Método para consultar todos los clientes.
        /// </summary>
        /// <returns></returns>
        public List<ClientDTO> GetAll(out string msError)
        {
            List<ClientDTO> listClients = new();
            try
            {
                msError = string.Empty;
                List<Client> listClient = _uow.Clients.GetAll().ToList();
                listClients = listClient
                    .Select(client => Mapper(client))
                    .OrderBy(client => client.IdClient).ToList();
            }
            catch (Exception ex)
            {
                msError = ex.Message;
            }

            return listClients;
        }

        /// <summary>
        /// Método para consultar clientes por id
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public ClientDTO GetById(int pId, out string msError)
        {
            ClientDTO client = new();

            try
            {
                msError = string.Empty;
                client = Mapper(_uow.Clients.GetById(pId));
            }
            catch (Exception ex)
            {
                msError = ex.Message;
            }

            return client;
        }

        /// <summary>
        /// Método para crear clientes.
        /// </summary>
        /// <param name="pClient"></param>
        /// <param name="msError"></param>
        /// <returns></returns>
        public bool Add(string pClient, out string msError) =>
            _uow.Clients.Add(JsonConvert.DeserializeObject<Client>(pClient), out msError);

        /// <summary>
        /// Método para actualizar clientes.
        /// </summary>
        /// <param name="pClient"></param>
        /// <param name="msError"></param>
        /// <returns></returns>
        public bool Update(string pClient, out string msError)
        {
            bool result = _uow.Clients.Update(
                JsonConvert.DeserializeObject<Client>(pClient),
                new Connection(_configuration).GetConnection(),
                out msError);
            return result;
        }

        /// <summary>
        /// Método para eliminar clientes por id.
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="msError"></param>
        /// <returns></returns>
        public bool Delete(int pId, out string msError) =>
            _uow.Clients.Delete(pId, out msError);

        /// <summary>
        /// Mapea una entidad en otra DTO
        /// </summary>
        private readonly Func<Client, ClientDTO> Mapper = (client) =>
        {
            ClientDTO clientDTO = new()
            {
                IdClient = client.IdClient,
                UserClient = client.UserClient,
                FirstName = client.FirstName,
                SecondName = client.SecondName,
                LastName = client.LastName,
                Charge = client.Charge,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                IdContract = (client.IdContractNavigation?.IdContract) ?? 0,
                Contract = (client.IdContractNavigation?.TypeContract) ?? string.Empty
            };

            return clientDTO;
        };
    }
}
