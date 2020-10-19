using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class CollectivesData
    {
        public IEnumerable<Collective> Collectives { get; set; }
        public IEnumerable<CollectiveUser> CollectiveUsers { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
