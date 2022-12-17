using Business.Intcomex.Interfaces;
using Entity.Intcomex.EntitiesDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using WebApplicationIntcomex.Configuration;
using WebApplicationIntcomex.Controllers;
using WebApplicationIntcomex.Models;

namespace UnitTest
{
    public class WebTesting
    {
        private readonly Mock<IOptions<MyConfig>> _config;
        private readonly Mock<IClientBO> _clientbo;
        private readonly Mock<IContractBO> _contract;
        private readonly ClientController _controller;
        public WebTesting()
        {
            _config = new Mock<IOptions<MyConfig>>();
            _clientbo = new Mock<IClientBO>();
            string msError = string.Empty;
            _clientbo.Setup(a => a.GetAll(out msError)).Returns(new List<ClientDTO>());
            _clientbo.Setup(a => a.GetById(1, out msError)).Returns(new ClientDTO() { IdClient = 1 });
            _clientbo.Setup(a => a.Add("Client{}", out msError)).Returns(true);
            _clientbo.Setup(a => a.Update("Client{idclient:1}", out msError)).Returns(true);
            _clientbo.Setup(a => a.Delete(1, out msError)).Returns(true);

            _contract = new Mock<IContractBO>();
            _controller = new ClientController(_config.Object, _clientbo.Object, _contract.Object);
        }

        [Fact]
        public void Get_Ok()
        {
            var result = _controller.GetAll();
            Assert.NotNull(result);
            Assert.IsType<PartialViewResult>(result);
        }

        [Fact]
        public void GetById_Ok()
        {
            int id = 1;
            var result = _controller.GetById(id);            
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.IsType<ClientDTO>(result.Value);

            var clientDTO = (ClientDTO)result.Value;
            Assert.True(clientDTO != null);
            Assert.Equal(clientDTO.IdClient, id);
        }

        [Fact]
        public void GetById_Err()
        {
            int id = 2;
            var result = _controller.GetById(id);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);            
            Assert.True(result.Value is null);
        }

        [Fact]
        public void Create_Ok()
        {
            var result = _controller.Create("Client{}");
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.IsType<ResponseDTO>(result.Value);

            var responseDTO = (ResponseDTO)result.Value;
            Assert.True(responseDTO != null);
            Assert.True(responseDTO.Status);
            Assert.Equal(responseDTO.Mesaje, "Customer successfully created");
        }

        [Fact]
        public void Create_Err()
        {
            var result = _controller.Create("");
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.IsType<ResponseDTO>(result.Value);

            var responseDTO = (ResponseDTO)result.Value;
            Assert.True(responseDTO != null);
            Assert.False(responseDTO.Status);
            Assert.Equal(responseDTO.Mesaje, "It was not possible to create the client");
        }

        [Fact]
        public void Update_Ok()
        {
            var client = "Client{idclient:1}";
            var result = _controller.Update(client);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.IsType<ResponseDTO>(result.Value);

            var responseDTO = (ResponseDTO)result.Value;
            Assert.True(responseDTO != null);
            Assert.True(responseDTO.Status);
            Assert.Equal(responseDTO?.Mesaje, "Client successfully updated");
        }

        [Fact]
        public void Update_Err()
        {
            var client = "Client{idclient:2}";
            var result = _controller.Update(client);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.IsType<ResponseDTO>(result.Value);

            var responseDTO = (ResponseDTO)result.Value;
            Assert.True(responseDTO != null);
            Assert.False(responseDTO.Status);
            Assert.Equal(responseDTO.Mesaje, "It was not possible to update the client");
        }

        [Fact]
        public void Delete_Ok()
        {
            int id = 1;
            var result = _controller.Delete(id);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.IsType<ResponseDTO>(result.Value);

            var responseDTO = (ResponseDTO)result.Value;
            Assert.True(responseDTO != null);
            Assert.True(responseDTO.Status);
            Assert.Equal(responseDTO?.Mesaje, "Customer successfully deleted");
        }

        [Fact]
        public void Delete_Err()
        {
            int id = 2;
            var result = _controller.Delete(id);
            Assert.NotNull(result);
            Assert.IsType<JsonResult>(result);
            Assert.IsType<ResponseDTO>(result.Value);

            var responseDTO = (ResponseDTO)result.Value;
            Assert.True(responseDTO != null);
            Assert.False(responseDTO.Status);
            Assert.Equal(responseDTO.Mesaje, "It was not possible to delete the client");
        }
    }
}
