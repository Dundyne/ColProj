using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class CollectiveUser
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int CollectiveId { get; set; }
        public Collective Collective { get; set; }
}
}
