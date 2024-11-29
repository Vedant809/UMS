using System.ComponentModel.DataAnnotations;

namespace UMS.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string email { get; set; }
        public string password { get; set; }
    }
}
