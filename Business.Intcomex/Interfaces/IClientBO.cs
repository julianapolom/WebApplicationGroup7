using Entity.Intcomex.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Intcomex.Interfaces
{
    public interface IClientBO
    {
        bool Add(string pClient);

        List<Client> GetAll();
    }
}
