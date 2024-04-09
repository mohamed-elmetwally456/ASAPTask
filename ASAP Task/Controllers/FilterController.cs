using ASAP_Task.DTO;
using ASAP_Task.Models;
using ASAP_Task.Reposatory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly IFilterRepo filterRepo;

        public FilterController(IFilterRepo filterRepo)
        {
            this.filterRepo = filterRepo;
        }
        [HttpGet("/api/FilterByFname/{fname:alpha}")]
        public ActionResult<IEnumerable<ClientsDto>> FilterByFname(string fname)
        {
            IEnumerable<Clients> Clients = filterRepo.FilterByFname(fname);
            if (Clients == null)
            {
                return NotFound();
            }
            List<ClientsDto> ClientsDtos = new List<ClientsDto>();

            foreach (var item in Clients)
            {
                ClientsDto ClientsDto = new ClientsDto()
                {
                    ClientId=item.ClientId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                };
                ClientsDtos.Add(ClientsDto);
            }
            return Ok(ClientsDtos);
        }
        [HttpGet("/api/FilterByLname/{lname:alpha}")]
        public ActionResult<IEnumerable<ClientsDto>> FilterByLname(string lname)
        {
            IEnumerable<Clients> Clients = filterRepo.FilterByLname(lname);
            if (Clients == null)
            {
                return NotFound();
            }
            List<ClientsDto> ClientsDtos = new List<ClientsDto>();

            foreach (var item in Clients)
            {
                ClientsDto ClientsDto = new ClientsDto()
                {
                    ClientId = item.ClientId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                };
                ClientsDtos.Add(ClientsDto);
            }
            return Ok(ClientsDtos);
        }
    }
}
