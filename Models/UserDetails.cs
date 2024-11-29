using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace UMS.Models
{
    public class UserDetails
    {
        public int UserId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string UserCode { get; set; } = string.Empty;
        public string Roles { get; set; }
        public bool IsActive { get; set; }
        public byte[] Picture { get; set; } = new byte[] { };

    }
}
