using ASAP_Task.DTO;
using ASAP_Task.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASAP_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }
        //[HttpPost("Send")]
        [ApiExplorerSettings(IgnoreApi =true)]
        public async Task <IActionResult> SendEmail([FromForm]EmailRequestDto Dto)
        {
            await emailService.SendEmailAsync(Dto.ToEmail, Dto.Subject, Dto.Body, Dto.Attachments);
            return Ok();
        }
    }
}
