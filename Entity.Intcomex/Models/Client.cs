namespace Entity.Intcomex.Models
{
    public partial class Client
    {
        public int IdClient { get; set; }

        public string? UserClient { get; set; }

        public string? FirstNombre { get; set; }

        public string? SecondName { get; set; }

        public string? LastName { get; set; }

        public string? Charge { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public int? IdContract { get; set; }

        public virtual ContractClient? IdContractNavigation { get; set; }
    }
}
