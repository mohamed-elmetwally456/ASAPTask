﻿using System.ComponentModel.DataAnnotations;

namespace ASAP_Task.DTO
{
    public class EmailRequestDto
    {
        [Required]
        public string ToEmail { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public IList<IFormFile> Attachments { get; set; }

    }
}
