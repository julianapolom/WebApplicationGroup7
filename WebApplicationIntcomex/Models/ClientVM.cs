using Entity.Intcomex.Models;

namespace WebApplicationIntcomex.Models
{
    public class ClientVM
    {
        public new ClientDTO modelPopUp { get; set; }
        public List<ClientDTO> ListClients { get; set; }
    }

    public class ClientDTO : Client
    {
        public string FullName
        {
            get => FirstName + " " + (string.IsNullOrEmpty(SecondName) ? string.Empty : SecondName) + " " + LastName;
        }

        public string Contract { get; set; }
    }
}
