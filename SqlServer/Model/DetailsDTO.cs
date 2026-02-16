using System.ComponentModel.DataAnnotations;

namespace SqlServer.Model
{
    public class DetailsDTO
    {
        [Required]
        public string otp { get; set; }

        [Required]
        public string message { get; set; }
    }
}
