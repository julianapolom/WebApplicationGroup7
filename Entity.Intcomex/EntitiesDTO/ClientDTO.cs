using Entity.Intcomex.Models;
using System.ComponentModel.DataAnnotations;

namespace Entity.Intcomex.EntitiesDTO
{
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
