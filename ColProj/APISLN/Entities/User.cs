using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApi.Entities
{
    public class User
    {
        public int Id { get; set; }

        [RegularExpression(@"^[A-Za-z0-9]{1,15}$",
     ErrorMessage = "Invalid First Name - Special characters not allowed")]
        [Required]
        public string FirstName { get; set; }

        [RegularExpression(@"^[A-Za-z0-9]{1,15}$",
     ErrorMessage = "Invalid Last Name - Special characters not allowed")]
        [Required]
        public string LastName { get; set; }

        [RegularExpression(@"^[A-Za-z0-9]{5,15}$",
     ErrorMessage = "Invalid Username - Special characters not allowed")]
        [Required]
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
        [JsonIgnore]
        public ICollection<CollectiveUser> CollectiveUsers { get; set; }
    }
}