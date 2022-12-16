using Business.Intcomex.Interfaces;
using Entity.Intcomex.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
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

        /// <summary>
        /// Constructor de la clase.
        /// </summary>
        /// <param name="config">Inyección de la configuración</param>
        /// <param name="clientbo">Inyección del cliente</param>
        /// <param name="contract">Inyección del contrato</param>
        public ClientController(IOptions<MyConfig> config, IClientBO clientbo, IContractBO contract)
        {
            _config = config;
            _clientbo = clientbo;
            _contract = contract;
        }

        /// <summary>
        /// Carga la pantalla inicial.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            LoadViewBags();
            ClientVM model = new() { ListClients = new List<ClientDTO>() };
            model.ListClients = _clientbo.GetAll(out string msError);
            ChatError(msError);
            return View(model);
        }

        /// <summary>
        /// Optiene los clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAll()
        {
            List<ClientDTO> ListClients = _clientbo.GetAll(out string msError);
            ChatError(msError);
            return PartialView("_Clients", ListClients);
        }

        /// <summary>
        /// Optiene el cliente por id
        /// </summary>
        /// <param name="pId">id a filtrar</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetById(int pId)
        {
            ClientDTO client = _clientbo.GetById(pId, out string msError);
            ChatError(msError);
            return Json(client);
        }

        /// <summary>
        /// Registra un cliente en base de datos.
        /// </summary>
        /// <param name="pModel"></param>
        /// <returns></returns>
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
            ChatError(msError);
            return Json(result);
        }

        /// <summary>
        /// Actualiza un cliente en base de datos.
        /// </summary>
        /// <param name="pModel"></param>
        /// <returns></returns>
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
            ChatError(msError);
            return Json(result);
        }

        /// <summary>
        /// Elimina clientes por id.
        /// </summary>
        /// <param name="pId">id a filtrar</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int pId)
        {
            ResponseDTO result = new()
            {
                Status = _clientbo.Delete(pId, out string msError)
            };

            result.Mesaje = result.Status ? "Customer successfully deleted" : "It was not possible to delete the client";
            ChatError(msError);
            return Json(result);
        }

        #endregion

        #region Métodos privados

        /// <summary>
        /// Escribe en el log.txt en caso de error.
        /// </summary>
        /// <param name="msError"></param>
        private void ChatError(string msError)
        {
            if (!string.IsNullOrEmpty(msError))
                Log.GetInstance(_config.Value.PathLog).Save(msError);
        }

        /// <summary>
        /// Método para cargar los viewbags
        /// </summary>
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
