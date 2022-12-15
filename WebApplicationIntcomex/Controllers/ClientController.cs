using Business.Intcomex.Interfaces;
using Entity.Intcomex.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Drawing;
using WebApplicationIntcomex.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace WebApplicationIntcomex.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientBO _clientbo;
        private readonly IContractBO _contract;
        public ClientController(IClientBO clientbo, IContractBO contract)
        {
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

            model.ListClients = _clientbo.GetAll().Select(x => new ClientDTO
            {
                UserClient = x.UserClient,
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                LastName = x.LastName,
                Charge = x.Charge,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email
            }).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<ClientDTO> ListClients = _clientbo.GetAll().Select(x => new ClientDTO
            {
                UserClient = x.UserClient,
                FirstName = x.FirstName,
                SecondName = x.SecondName,
                LastName = x.LastName,
                Charge = x.Charge,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email
            }).ToList();

            return PartialView("_Clients", ListClients);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(string pModel)
        {
            ResponseDTO result = new()
            {
                Estado = !string.IsNullOrEmpty(pModel) && _clientbo.Add(pModel)
            };

            result.Mensaje = result.Estado ? "Client succes" : "Error";
            return Json(result);
        }

        private void LoadViewBags()
        {
            ViewBag.Contracts = _contract.GetAll()
               .Select(s => new SelectListItem
               {
                   Text = s.TypeContract,
                   Value = s.IdContract.ToString()
               });
        }
    }
}
