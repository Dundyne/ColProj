using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Collective
    {


        public int Id { get; set; }
        public int OwnerId { get; set; }
        [Required]
        public int Size { get; set; }
        [RegularExpression(@"^[A-Za-z0-9]{1,15}$",
     ErrorMessage = "Invalid Name - Special characters not allowed")]
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^[A-Za-z0-9]{1,30}$",
     ErrorMessage = "Invalid Description - Special characters not allowed")]
        [Required]
        public string Description { get; set; }
        public ICollection<Post> Posts { get; set; }
        [JsonIgnore]
        public ICollection<CollectiveUser> CollectiveUsers { get; set; }

    }
}
