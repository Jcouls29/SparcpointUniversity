using System;
using System.Collections.Generic;

namespace SparcpointUniversity.Console
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedTimestamp { get; set; }

        public Dictionary<string, string> Attributes { get; set; }
    }
}
