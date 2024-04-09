using ASAP_Task.Models;
using System.ComponentModel.DataAnnotations;

namespace ASAP_Task.DTO
{
    public class ClientsDto
    {
        public int ClientId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Phone should start with 010 or 011 or 012 or 015 then 8 digits ")]
        public string PhoneNumber { get; set; }
    }
}
