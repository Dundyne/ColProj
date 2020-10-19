using System;
using System.Collections.Generic;
using System.Text;

namespace WebUwp.Core.Models
{
    public class Collective
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
