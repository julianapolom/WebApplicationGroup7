using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entity.Intcomex.Models;

public partial class ContractClient
{
    [Key]
    public int IdContract { get; set; }

    public string? TypeContract { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();
}
