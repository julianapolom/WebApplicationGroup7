using System;
using System.Collections.Generic;

namespace Entity.Intcomex.Models;

public partial class ContractClient
{
    public int IdContract { get; set; }

    public string? TypeContract { get; set; }

    public virtual ICollection<Client> Clients { get; } = new List<Client>();
}
