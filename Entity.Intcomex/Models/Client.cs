using System.ComponentModel.DataAnnotations;

namespace Entity.Intcomex.Models
{
    public partial class Client
    {
        [Key]
        public int IdClient { get; set; }

        [Display(Name = "User Client")]
        [StringLength(50)]
        public string UserClient { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Second Name")]
        [StringLength(50)]
        public string? SecondName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Charge")]
        [StringLength(50)]
        public string Charge { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [StringLength(150)]
        public string Email { get; set; }
        public int? IdContract { get; set; }        
        public virtual ContractClient? IdContractNavigation { get; set; }        
    }
}
