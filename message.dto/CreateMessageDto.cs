using System.ComponentModel.DataAnnotations;

namespace message.dto
{
    public class CreateMessageDto
    {
        [Required]
        public string Body { get; set; }
        [Required]
        public string To { get; set; }
    }
}