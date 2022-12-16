using Business.Intcomex.Interfaces;
using Entity.Intcomex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Tools;
using WebApplicationIntcomex.Configuration;
using WebApplicationIntcomex.Models;

namespace WebApplicationIntcomex.Controllers
{
    public class ClientController : Controller
    {
        #region Variables y objetos globales

        private readonly IOptions<MyConfig> _config;
        private readonly IClientBO _clientbo;
        private readonly IContractBO _contract;

        #endregion

        #region Métodos públicos
        public ClientController(IOptions<MyConfig> config, IClientBO clientbo, IContractBO contract)
        {
            _config = config;
            _clientbo = clientbo;
            _contract = contract;
        }

        [HttpGet]
        public IActionResult Index()
        {
            LoadViewBags();
            ClientVM model = new()
            {
                ListClients = new List<ClientDTO>()
            };

            model.ListClients = _clientbo.GetAll().Select(client => Merge(client)).ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<ClientDTO> ListClients = _clientbo.GetAll().Select(client => Merge(client)).ToList();
            return PartialView("_Clients", ListClients);
        }

        [HttpGet]
        public async Task<JsonResult> GetById(int pId)
        {
            ClientDTO client = Merge(await _clientbo.GetById(pId));
            return Json(client);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(string pModel)
        {
            string msError = string.Empty;
            ResponseDTO result = new()
            {
                Status = !string.IsNullOrEmpty(pModel) && _clientbo.Add(pModel, out msError)
            };

            result.Mesaje = result.Status ? "Customer successfully created" : "It was not possible to create the client";
            if (!string.IsNullOrEmpty(msError))
                Log.GetInstance(_config.Value.PathLog).Save(msError);

            return Json(result);
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public JsonResult Update(string pModel)
        {
            string msError = string.Empty;
            ResponseDTO result = new()
            {
                Status = !string.IsNullOrEmpty(pModel) && _clientbo.Update(pModel, out msError)
            };

            result.Mesaje = result.Status ? "Client successfully updated" : "It was not possible to update the client";
            if (!string.IsNullOrEmpty(msError))
                Log.GetInstance(_config.Value.PathLog).Save(msError);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int pId)
        {
            ResponseDTO result = new()
            {
                Status = _clientbo.Delete(pId, out string msError)
            };

            result.Mesaje = result.Status ? "Customer successfully deleted" : "It was not possible to delete the client";
            if (!string.IsNullOrEmpty(msError))
                Log.GetInstance(_config.Value.PathLog).Save(msError);

            return Json(result);
        }

        #endregion

        #region Métodos privados

        private Func<Client, ClientDTO> Merge = (client) =>
        {
            ClientDTO clientDTO = new ClientDTO
            {
                IdClient= client.IdClient,
                UserClient = client.UserClient,
                FirstName = client.FirstName,
                SecondName = client.SecondName,
                LastName = client.LastName,
                Charge = client.Charge,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                IdContract = client.IdContractNavigation.IdContract,
                Contract = client.IdContractNavigation.TypeContract
            };

            return clientDTO;
        };

        private void LoadViewBags()
        {
            ViewBag.Contracts = _contract.GetAll()
               .Select(s => new SelectListItem
               {
                   Text = s.TypeContract,
                   Value = s.IdContract.ToString()
               });
        }

        #endregion
    }
}
