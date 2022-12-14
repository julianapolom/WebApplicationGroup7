using Business.Intcomex.Interfaces;
using Entity.Intcomex.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationIntcomex.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientBO _clientbo;
        public ClientController(IClientBO clientbo)
        {
            _clientbo = clientbo;
        }
        public IActionResult Index()
        {
            var prueba = _clientbo.GetAll();
            return View();
        }

        public IActionResult Add(Client pModel)
        {
            _clientbo.Add(pModel);
            return View();
        }
    }
}
