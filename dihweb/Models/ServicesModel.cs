using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dihweb.Models
{
    public class ServicesModel
    {
        public int count { get; set; }
        public List<Service> services { get; set; }
    }

    public class Service
    {
        public List<object> commands { get; set; }
        public List<object> lazy { get; set; }
        public List<object> attributes { get; set; }
        public string _id { get; set; }
        public string resource { get; set; }
        public string apikey { get; set; }
        public string service { get; set; }
        public string subservice { get; set; }
        public int __v { get; set; }
        public List<object> static_attributes { get; set; }
        public List<object> internal_attributes { get; set; }
        public string entity_type { get; set; }
    }
}
