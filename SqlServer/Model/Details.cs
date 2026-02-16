using System.ComponentModel.DataAnnotations;

namespace SqlServer.Model
{
    public class Details
    {
        [Key]
        public int employeeID { get; set; }

        [Required]
        public string otp { get; set; }
        public DateTime? ExpiryTime { get; set; }

        public string name { get; set; }

    }
}
