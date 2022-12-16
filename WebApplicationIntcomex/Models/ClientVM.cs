using Entity.Intcomex.Models;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Contract")]
        public int IdContract { get; set; }
        public string Contract { get; set; }
    }
}
