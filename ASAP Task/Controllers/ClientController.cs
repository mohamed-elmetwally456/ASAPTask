using ASAP_Task.DTO;
using ASAP_Task.Mapper;
using ASAP_Task.Models;
using ASAP_Task.Reposatory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepo clientRepo;

        public ClientController(IClientRepo clientRepo)
        {
            this.clientRepo = clientRepo;
        }
        [HttpGet("/api/GetByPaging")]
        public async Task<ActionResult<ApiResponse>> GetClients(int pageIndex = 1, int pageSize = 10)
        {
            var clients = await clientRepo.GetClients(pageIndex, pageSize);
            return new ApiResponse(true, null, clients);
        }
        [HttpGet("/api/GetAll")]
        public ActionResult<IEnumerable<ClientsDto>> GetAll() 
        {
            List<ClientsDto> ClientsDto =new List<ClientsDto>();
            foreach (var item in clientRepo.GetAll())
            {
                ClientsDto clientDto = new ClientsDto()
                {
                    ClientId=item.ClientId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email, 
                    PhoneNumber = item.PhoneNumber,
                };
                ClientsDto.Add(clientDto);
            }
            return Ok(ClientsDto);
        }
        [HttpGet("{id:int}")]
        public ActionResult GetByID(int id)
        {
            Clients client = clientRepo.GetByID(id);
            if (client == null)
            {
                return NotFound();
            }
            ClientsDto clientDto = new ClientsDto()
            {
                ClientId = client.ClientId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
            };
            return Ok(clientDto);
        }
        [HttpPost]
        public ActionResult Add(ClientsDto clientDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var Client = new Clients
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                PhoneNumber = clientDto.PhoneNumber,
            };
            clientRepo.Add(Client);
            return CreatedAtAction("GetByID", new { id = Client.ClientId }, clientDto);
        }
        [HttpPut("{id}")]
        public ActionResult Update(ClientsDto clientDto, int id)
        {
            if (clientDto.ClientId != id) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var client = new Clients
            {
                ClientId =clientDto.ClientId,
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                PhoneNumber = clientDto.PhoneNumber,
            };
            clientRepo.Update(client);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingClient = clientRepo.GetByID(id);
            if (existingClient == null)
            {
                return NotFound();
            }
            clientRepo.Delete(id);
            return Ok();
        }
    }
}
