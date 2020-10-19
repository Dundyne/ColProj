using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        [RegularExpression(@"^[A-Za-z0-9]{1,15}$",
     ErrorMessage = "Invalid Title Name - Special characters not allowed")]
        [Required]
        public string Title { get; set; }
        [RegularExpression(@"^[A-Za-z0-9?=!.,*]{1,}$",
                 ErrorMessage = "Invalid Content")]
        [Required]
        public string Content { get; set; }
        public int CollectiveId { get; set; }
        public Collective Collective { get; set; }
    }
}
