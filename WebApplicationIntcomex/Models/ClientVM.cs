using Entity.Intcomex.EntitiesDTO;

namespace WebApplicationIntcomex.Models
{
    public class ClientVM
    {
        public new ClientDTO modelPopUp { get; set; }
        public List<ClientDTO> ListClients { get; set; }
    }    
}
